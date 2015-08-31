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
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Common {

public sealed class SamplinglongPrimitiveIteratorTest : TasteTestCase {

  [Test]
  public void testEmptyCase() {
    Assert.False(new SamplinglongPrimitiveIterator(
        countingIterator(0), 0.9999).MoveNext());
    Assert.False(new SamplinglongPrimitiveIterator(
        countingIterator(0), 1).MoveNext());
  }

  [Test]
  public void testSmallInput() {
    SamplinglongPrimitiveIterator t = new SamplinglongPrimitiveIterator(
        countingIterator(1), 0.9999);
    Assert.True(t.MoveNext());
    Assert.AreEqual(0L, t.Current);
    Assert.False(t.MoveNext());
  }

  [Test]
	[ExpectedException(typeof( ArgumentException) )]
	//(expected = IllegalArgumentException.class)
  public void testBadRate1() {
    new SamplinglongPrimitiveIterator(countingIterator(1), 0.0);
  }

  [Test] //(expected = IllegalArgumentException.class)
  [ExpectedException(typeof(ArgumentException))]
  public void testBadRate2() {
    new SamplinglongPrimitiveIterator(countingIterator(1), 1.1);
  }

  [Test]
  public void testExactSizeMatch() {
    SamplinglongPrimitiveIterator t = new SamplinglongPrimitiveIterator(
        countingIterator(10), 1);
    for (int i = 0; i < 10; i++) {
      Assert.True(t.MoveNext());
      Assert.AreEqual(i, (int)t.Current );
    }
    Assert.False(t.MoveNext());
  }

  [Test]
  public void testSample() {

    double p = 0.1;
    int n = 1000;
    double sd = Math.Sqrt(n * p * (1.0 - p));
    for (int i = 0; i < 1000; i++) {
      SamplinglongPrimitiveIterator t = new SamplinglongPrimitiveIterator(countingIterator(n), p);
      int k = 0;
      while (t.MoveNext()) {
        long v = t.Current;
        k++;
        Assert.True(v >= 0L);
        Assert.True(v < 1000L);
      }
      // Should be +/- 5 standard deviations except in about 1 out of 1.7M cases
	  Assert.True(k >= 100 - 5 * sd);
      Assert.True(k <= 100 + 5 * sd);
    }
  }

  private static IEnumerator<long> countingIterator(int to) {
    long[] data = new long[to];
    for (int i = 0; i < to; i++) {
      data[i] = i;
    }
    return ((IEnumerable<long>)data).GetEnumerator();
  }

}
}