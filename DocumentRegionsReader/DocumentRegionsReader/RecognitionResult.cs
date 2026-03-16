using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DocumentRegionsReader
{
    class RecognitionResult
    {
        public Rectangle bounds{get; private set;}
        public string description { get; private set; }
        public int pageNum { get; private set; }
        public int indexNum { get; private set; }
        private string results;
        public float OmrRatio { get; set; }

        public RecognitionResult(Rectangle rect, string name, int page, int index)
        {
            bounds = rect;
            description = name;
            pageNum = page;
            indexNum = index;
        
        }

        public void setResults(string p)
        {
            results = p;
        
        }
        public string getResults()
        {

            return results;
        }
    }
}
