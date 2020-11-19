using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerQueue2
{
    class Program
    {
        public void ProcessDocumentsUsingProducerConsumerPattern()
        {
            string[] documentIds = GetDocumentIdsToProcess();

            BlockingCollection inputQueue = CreateInputQueue(documentIds);

            BlockingCollection queue = new BlockingCollection(500);

            var consumer = Task.Run(() =>
            {
                foreach (var translatedDocument in queue.GetConsumingEnumerable())
                {
                    SaveDocumentToDestinationStore(translatedDocument);
                }
            });

            var producers = Enumerable.Range(0, 7)
                .Select(_ => Task.Run(() =>
                {
                    foreach (var documentId in inputQueue.GetConsumingEnumerable())
                    {
                        var document = ReadAndTranslateDocument(documentId);
                        queue.Add(document);
                    }
                }))
                .ToArray();

            Task.WaitAll(producers);

            queue.CompleteAdding();

            consumer.Wait();
        }
        private BlockingCollection CreateInputQueue(string[] documentIds)
        {
            var inputQueue = new BlockingCollection();

            foreach (var id in documentIds)
                inputQueue.Add(id);

            inputQueue.CompleteAdding();

            return inputQueue;
        }
        void ProcessDocuments()
        {
            string[] documentIds = GetDocumentIdsToProcess();

            foreach (var id in documentIds)
            {
                Process(id);
            }
        }
        string[] GetDocumentIdsToProcess() {
           string[] ids = new string[] { "id1", "id2" , "id3"};
            return ids;     
        }
        //void Process(string documentId)
        //{
        //    var document = ReadDocumentFromSourceStore(documentId);

        //    var translatedDocument = TranslateDocument(document, Language.English);

        //    SaveDocumentToDestinationStore(translatedDocument);
        //}

        Semaphore semaphore = new Semaphore(2, 2);

        public void Process(string documentId)
        {
            semaphore.WaitOne();

            Document document;

            try
            {
                document = ReadDocumentFromSourceStore(documentId);
            }
            finally
            {
                semaphore.Release();
            }

            var translatedDocument = TranslateDocument(document, Language.English);

            SaveDocumentToDestinationStore(translatedDocument);
        }
    }
}
