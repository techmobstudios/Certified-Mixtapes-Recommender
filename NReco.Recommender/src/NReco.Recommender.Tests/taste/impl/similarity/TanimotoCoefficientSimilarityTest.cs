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
using NReco.CF.Taste.Model;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Similarity {

/// <p>Tests {@link TanimotoCoefficientSimilarity}.</p> 
public sealed class TanimotoCoefficientSimilarityTest : SimilarityTestCase {

  [Test]
  public void testNoCorrelation() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{null, 2.0, 3.0},
                    new double?[]{1.0},
            });
    double correlation = new TanimotoCoefficientSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(Double.NaN, correlation);
  }

  [Test]
  public void testFullCorrelation1() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{1.0},
                    new double?[]{1.0},
            });
    double correlation = new TanimotoCoefficientSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(1.0, correlation);
  }

  [Test]
  public void testFullCorrelation2() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{1.0, 2.0, 3.0},
                    new double?[]{1.0},
            });
    double correlation = new TanimotoCoefficientSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(0.3333333333333333, correlation);
  }

  [Test]
  public void testCorrelation1() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{null, 2.0, 3.0},
                    new double?[]{1.0, 1.0},
            });
    double correlation = new TanimotoCoefficientSimilarity(dataModel).UserSimilarity(1, 2);
    Assert.AreEqual(0.3333333333333333, correlation, EPSILON);
  }

  [Test]
  public void testCorrelation2() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{null, 2.0, 3.0, 1.0},
                    new double?[]{1.0, 1.0, null, 0.0},
            });
    double correlation = new TanimotoCoefficientSimilarity(dataModel).UserSimilarity(1, 2);
    Assert.AreEqual(0.5, correlation, EPSILON);
  }

  [Test]
  public void testRefresh() {
    // Make sure this doesn't throw an exception
    new TanimotoCoefficientSimilarity(getDataModel()).Refresh(null);
  }

  [Test]
  public void testReturnNaNDoubleWhenNoSimilaritiesForTwoItems() {
	  IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{null, null, 3.0},
                    new double?[]{1.0, 1.0, null},
            });  
	  Double similarity = new TanimotoCoefficientSimilarity(dataModel).ItemSimilarity(1, 2);
	  Assert.AreEqual(Double.NaN, similarity, EPSILON);
  }
  
  [Test]
  public void testItemsSimilarities() {
	  IDataModel dataModel = getDataModel(
	            new long[] {1, 2},
	            new Double?[][] {
	                    new double?[]{2.0, null, 2.0},
	                    new double?[]{1.0, 1.0, 1.0},
	            });  
	  TanimotoCoefficientSimilarity tCS = new TanimotoCoefficientSimilarity(dataModel);
	  Assert.AreEqual(0.5, tCS.ItemSimilarity(0, 1), EPSILON);
	  Assert.AreEqual(1, tCS.ItemSimilarity(0, 2), EPSILON);
	  
	  double[] similarities = tCS.ItemSimilarities(0, new long [] {1, 2});
	  Assert.AreEqual(0.5, similarities[0], EPSILON);
	  Assert.AreEqual(1, similarities[1], EPSILON);
  }
  
}
}