using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentRegionsReader
{
    class Results
    {
        public int Count;
        private List<RecognitionResult> list = new List<RecognitionResult>();
        public Results()
        {
            Count = 0;
        }

        public void Add(RecognitionResult region)
        {
            list.Add(region);
            Count++;
        
        }
        public RecognitionResult Get(int page, int index)
        {
            foreach (RecognitionResult result in list)
            {
                if (result.indexNum == index && result.pageNum == page)
                    return result;
                
            
            }
            return null;
        
        }
        public int GetCountForPage(int pageNumber)
        {
            int count = 0;
            foreach (RecognitionResult result in list)
            {
                if (result.pageNum == pageNumber)
                    count++;
            
            }

            return count;
        
        }
        public RecognitionResult[] GetRegionsForPage(int pageNumber)
        {
            RecognitionResult[] results = new RecognitionResult[GetCountForPage(pageNumber)];
            int i = 0;
            foreach (RecognitionResult result in list)
            {
                if (result.pageNum == pageNumber)
                {
                    results[i] = result;
                    i++;
                }
            }

            return results;
        }

    }
}
