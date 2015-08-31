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
using NReco.CF.Taste.Impl;
using NReco.CF.Taste.Model;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Model {

public sealed class GenericUserPreferenceArrayTest : TasteTestCase {

  [Test]
  public void testUserID() {
    IPreferenceArray prefs = new GenericUserPreferenceArray(3);
    Assert.AreEqual(3, prefs.Length());
    prefs.SetUserID(0, 1L);
    Assert.AreEqual(1L, prefs.GetUserID(0));
    Assert.AreEqual(1L, prefs.GetUserID(1));
    Assert.AreEqual(1L, prefs.GetUserID(2));
  }

  [Test]
  public void testItemID() {
    IPreferenceArray prefs = new GenericUserPreferenceArray(3);
    Assert.AreEqual(3, prefs.Length());
    prefs.SetItemID(0, 1L);
    prefs.SetItemID(1, 2L);
    prefs.SetItemID(2, 3L);
    Assert.AreEqual(1L, prefs.GetItemID(0));
    Assert.AreEqual(2L, prefs.GetItemID(1));
    Assert.AreEqual(3L, prefs.GetItemID(2));    
  }

  [Test]
  public void testSetValue() {
    IPreferenceArray prefs = new GenericUserPreferenceArray(3);
    Assert.AreEqual(3, prefs.Length());
    prefs.SetValue(0, 1.0f);
    prefs.SetValue(1, 2.0f);
    prefs.SetValue(2, 3.0f);
    Assert.AreEqual(1.0f, prefs.GetValue(0), EPSILON);
    Assert.AreEqual(2.0f, prefs.GetValue(1), EPSILON);
    Assert.AreEqual(3.0f, prefs.GetValue(2), EPSILON);
  }

  [Test]
  public void testHasPref() {
    IPreferenceArray prefs = new GenericUserPreferenceArray(3);
    prefs.Set(0, new GenericPreference(1L, 3L, 5.0f));
    Assert.True(prefs.HasPrefWithItemID(3L));
    Assert.True(prefs.HasPrefWithUserID(1L));
    Assert.False(prefs.HasPrefWithItemID(2L));
    Assert.False(prefs.HasPrefWithUserID(2L));
  }

  [Test]
  public void testSort() {
    IPreferenceArray prefs = new GenericUserPreferenceArray(3);
    prefs.Set(0, new GenericPreference(1L, 3L, 5.0f));
    prefs.Set(1, new GenericPreference(1L, 1L, 5.0f));
    prefs.Set(2, new GenericPreference(1L, 2L, 5.0f));
    prefs.SortByItem();
    Assert.AreEqual(1L, prefs.GetItemID(0));
    Assert.AreEqual(2L, prefs.GetItemID(1));
    Assert.AreEqual(3L, prefs.GetItemID(2));    
  }

  [Test]
  public void testSortValue() {
    IPreferenceArray prefs = new GenericUserPreferenceArray(3);
    prefs.Set(0, new GenericPreference(1L, 3L, 5.0f));
    prefs.Set(1, new GenericPreference(1L, 1L, 4.0f));
    prefs.Set(2, new GenericPreference(1L, 2L, 3.0f));
    prefs.SortByValue();
    Assert.AreEqual(2L, prefs.GetItemID(0));
    Assert.AreEqual(1L, prefs.GetItemID(1));
    Assert.AreEqual(3L, prefs.GetItemID(2));
    prefs.SortByValueReversed();
    Assert.AreEqual(3L, prefs.GetItemID(0));
    Assert.AreEqual(1L, prefs.GetItemID(1));
    Assert.AreEqual(2L, prefs.GetItemID(2));
  }

  [Test]
  public void testClone() {
    GenericUserPreferenceArray prefs = new GenericUserPreferenceArray(3);
    prefs.Set(0, new GenericPreference(1L, 3L, 5.0f));
    prefs.Set(1, new GenericPreference(1L, 1L, 4.0f));
    prefs.Set(2, new GenericPreference(1L, 2L, 3.0f));
    prefs = (GenericUserPreferenceArray)prefs.Clone();
    Assert.AreEqual(3L, prefs.GetItemID(0));
    Assert.AreEqual(1L, prefs.GetUserID(1));
    Assert.AreEqual(3.0f, prefs.GetValue(2), EPSILON);
  }

}
}