/*
 *  Copyright 2013-2014 Vitalii Fedorchenko (nrecosite.com)
 *
 *  Licensed under Source Code Licence (see LICENSE file).
 *  
 *  Parts of this code are based on Apache Mahout ("Taste") that was licensed under the
 *  Apache 2.0 License (see http://www.apache.org/licenses/LICENSE-2.0).
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS 
 *  OF ANY KIND, either express or implied.
 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using NReco.CF.Taste.Impl;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Model {

 /// Tests {@link GenericDataModel}.
public sealed class GenericDataModelTest : TasteTestCase {

  [Test]  
  public void testSerialization() {
    GenericDataModel model = (GenericDataModel) getDataModel();
    
	  var formatter = new BinaryFormatter();
	  var ms = new MemoryStream();
		formatter.Serialize(ms, model);
	  ms.Position = 0;

	  GenericDataModel newModel = (GenericDataModel)formatter.Deserialize(ms);

    Assert.AreEqual(model.GetNumItems(), newModel.GetNumItems());
    Assert.AreEqual(model.GetNumUsers(), newModel.GetNumUsers());
    Assert.True(model.GetPreferencesFromUser(1L).Equals( newModel.GetPreferencesFromUser(1L) ) );    
    Assert.True(model.GetPreferencesForItem(1L).Equals(newModel.GetPreferencesForItem(1L) ) );
    Assert.AreEqual(model.GetRawUserData(), newModel.GetRawUserData());
  }

  // Lots of other stuff should be tested but is kind of covered by FileDataModelTest

}

}