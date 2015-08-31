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

using NReco.CF.Taste.Common;
using NReco.CF.Taste.Impl;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Common {


/// Tests {@link RefreshHelper} 
public sealed class RefreshHelperTest : TasteTestCase {

  [Test]
  public void testCallable() {
    MockRefreshable mock = new MockRefreshable();
    IRefreshable helper = new RefreshHelper(mock.call);
    helper.Refresh(null);
    Assert.AreEqual(1, mock.getCallCount());
  }

  [Test]
  public void testNoCallable() {
    IRefreshable helper = new RefreshHelper(null);
    helper.Refresh(null);
  }

  [Test]
  public void testDependencies() {
    RefreshHelper helper = new RefreshHelper(null);
    MockRefreshable mock1 = new MockRefreshable();
    MockRefreshable mock2 = new MockRefreshable();
    helper.AddDependency(mock1);
    helper.AddDependency(mock2);
    helper.Refresh(null);
    Assert.AreEqual(1, mock1.getCallCount());
    Assert.AreEqual(1, mock2.getCallCount());
  }

  [Test]
  public void testAlreadyRefreshed() {
    RefreshHelper helper = new RefreshHelper(null);
    MockRefreshable mock1 = new MockRefreshable();
    MockRefreshable mock2 = new MockRefreshable();
    helper.AddDependency(mock1);
    helper.AddDependency(mock2);
    IList<IRefreshable> alreadyRefreshed = new List<IRefreshable>(1);
    alreadyRefreshed.Add(mock1);
    helper.Refresh(alreadyRefreshed);
    Assert.AreEqual(0, mock1.getCallCount());
    Assert.AreEqual(1, mock2.getCallCount());
  }

}

}