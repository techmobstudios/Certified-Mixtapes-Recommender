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

public sealed class BitSetTest : TasteTestCase {

  private static int NUM_BITS = 100;

  [Test]
  public void testGetSet() {
    BitSet bitSet = new BitSet(NUM_BITS);
    for (int i = 0; i < NUM_BITS; i++) {
      Assert.False(bitSet.Get(i));

    }
    bitSet.Set(0);
    bitSet.Set(NUM_BITS-1);
    Assert.True(bitSet.Get(0));
    Assert.True(bitSet.Get(NUM_BITS-1));
  }

  //[Test](expected = ArrayIndexOutOfBoundsException.class)
	[Test]
	[ExpectedException]
  public void testBounds1() {
    BitSet bitSet = new BitSet(NUM_BITS);
    bitSet.Set(1000);
  }

  //[Test](expected = ArrayIndexOutOfBoundsException.class)\
	[Test]
	[ExpectedException]
  public void testBounds2() {
    BitSet bitSet = new BitSet(NUM_BITS);
    bitSet.Set(-1);
  }

	  [Test]
  public void testClear() {
    BitSet bitSet = new BitSet(NUM_BITS);
    for (int i = 0; i < NUM_BITS; i++) {
      bitSet.Set(i);
    }
    for (int i = 0; i < NUM_BITS; i++) {
      Assert.True(bitSet.Get(i));
    }
    bitSet.Clear();
    for (int i = 0; i < NUM_BITS; i++) {
      Assert.False(bitSet.Get(i));
    }
  }

  [Test]
  public void testClone() {
    BitSet bitSet = new BitSet(NUM_BITS);
    bitSet.Set(NUM_BITS-1);
    bitSet = (BitSet)bitSet.Clone();
    Assert.True(bitSet.Get(NUM_BITS-1));
  }

}
}