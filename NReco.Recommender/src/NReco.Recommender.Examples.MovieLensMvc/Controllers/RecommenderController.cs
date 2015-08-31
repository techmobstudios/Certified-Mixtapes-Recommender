using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Script.Serialization;

using System.Web.Security;

using NReco.CF.Taste.Model;
using NReco.CF.Taste.Impl.Model.File;
using NReco.CF.Taste.Impl.Eval;
using NReco.CF.Taste.Eval;
using NReco.CF.Taste.Impl.Similarity;
using NReco.CF.Taste.Impl.Neighborhood;
using NReco.CF.Taste.Impl.Recommender;
using NReco.CF.Taste.Impl.Recommender.SVD;
using NReco.CF.Taste.Impl.Model;
using NReco.CF.Taste.Recommender;

namespace Controllers {
	
	public class RecommenderController : Controller {

		public ActionResult RecommendFilmPage() {
			return View();
		}

		static IDataModel dataModel;

		public ActionResult Recommend(string filmIdsJson) {
			var filmIds = (new JavaScriptSerializer()).Deserialize<long[]>(filmIdsJson);
			var pathToDataFile =
					Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "data/albums.dat");

			if (dataModel == null) {
                try
                {
                    dataModel = new FileDataModel(pathToDataFile, false, FileDataModel.DEFAULT_MIN_RELOAD_INTERVAL_MS, false);
                }
                catch (Exception e)
                {
                    var exe = e.ToString();
                }
			}

			var plusAnonymModel = new PlusAnonymousUserDataModel(dataModel);
			var prefArr = new GenericUserPreferenceArray(filmIds.Length);
			prefArr.SetUserID(0, PlusAnonymousUserDataModel.TEMP_USER_ID);
			for (int i = 0; i < filmIds.Length; i++) {
				prefArr.SetItemID(i, filmIds[i]);
				prefArr.SetValue(i, 5); // lets assume max rating
			}
			plusAnonymModel.SetTempPrefs(prefArr);

			var similarity = new LogLikelihoodSimilarity(plusAnonymModel);
			var neighborhood = new NearestNUserNeighborhood(15, similarity, plusAnonymModel);
			var recommender = new GenericBooleanPrefUserBasedRecommender(plusAnonymModel, neighborhood, similarity);
			var recommendedItems = recommender.Recommend(PlusAnonymousUserDataModel.TEMP_USER_ID, 5, null);
			return Json( recommendedItems.Select(ri => new Dictionary<string, object>() {
				{"id", ri.GetItemID() },
				{"rating", ri.GetValue() },
			}).ToArray() );
		}



	}
}
