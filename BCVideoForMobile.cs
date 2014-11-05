using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;


namespace BrightcoveAPI
{
    // <summary>
    // The Video object is an aggregation of metadata and asset information associated with a video
    // </summary>
    [Serializable]
    public class BCVideoForMobile : BCVideo
    {
        private static Dictionary<long, int> mealData = null;
        private static System.DateTime lastUpdated = System.DateTime.Now;

        private string FLVURL;

        public String FlvUrl { get { return FLVURL; } }

        public int? VisionMealId { get {

            if (mealData == null || lastUpdated.AddDays(1) < System.DateTime.Now)
            {
                VisionPersonalTrainingProject.ClubVisionDataContext cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();
                var _meals = from meals in cvdc.Meals where meals.IsRecommended select meals;

                mealData = new Dictionary<long, int>();
                foreach (VisionPersonalTrainingProject.Meal _meal in _meals)
                {
                    if (_meal.VideoId != null)
                        try { mealData.Add((long)_meal.VideoId, _meal.Id); } catch { }
                }
                lastUpdated = System.DateTime.Now;
            }

            try
            {
                return mealData[this.ID];
            }
            catch
            {
                return null;
            }
        }
        }

    }
}