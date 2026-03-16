using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atalasoft.Imaging.ImageProcessing;
using Atalasoft.Imaging;
using System.Drawing;
using Atalasoft.Imaging.ImageProcessing.Document;

namespace ScratchLib
{
    public static class Reusables
    {
        /// <summary>
        /// Returns whether or not the ratio of black/white >= threshold
        /// </summary>
        /// <param name="image">AtalaImage to be tested</param>
        /// <param name="threshold">threshold ratio as a float, from 0 to 1 (inclusive)</param>
        /// <returns>True if above threshold</returns>
        public static bool runHistogram(AtalaImage image, float threshold)
        {
            return getHistogramRatio(image) >= threshold;
        }

        /// <summary>
        /// Returns whether or not the ratio of black/white > threshold
        /// </summary>
        /// <param name="image">AtalaImage to be tested</param>
        /// <param name="bounds">Rectangle bounds if only a subset of the image is to be tested.</param>
        /// <param name="threshold">threshold ratio as a float, from 0 to 1 (inclusive)</param>
        /// <returns>True if above threshold</returns>

        public static bool runHistogram(AtalaImage image, Rectangle bounds, int threshold)
        {
            return getHistogramRatio(image, bounds) >= threshold;
        }

        public static float getHistogramRatio(AtalaImage image, Rectangle bounds)
        {
            return getHistogramRatio(new CropCommand(bounds).Apply(image).Image);
        }

        public static float getHistogramRatio(AtalaImage image)
        {
            if (image.ColorDepth > 1)
            {
                DynamicThresholdCommand dtc = new DynamicThresholdCommand();
                image = dtc.Apply(image).Image;
            }


            Histogram hs = new Histogram(image);

            //White = "ffffffff";
            //Black = "ff000000";

            int[] docHist = hs.GetDocumentHistogram();

            float whiteCount, blackCount;

            if (image.Palette.GetEntry(0).Name == "ffffffff") //Typically, the first entry in our histogram is white, but it might not be. So, we're testing the index color to make sure.
            {
                whiteCount = docHist[0];
                blackCount = docHist[1];
            }
            else
            {
                blackCount = docHist[0];
                whiteCount = docHist[1];
            }

            if (whiteCount == 0) return float.MaxValue;

            return (float)(blackCount / (whiteCount + blackCount));
        }
    }
}
