using UnityEngine;
using System.Collections;

public class tests : MonoBehaviour {

	void Start () {
		TestRNG();	
			
	}

	void TestRNG() {
	
		PM_PRNG rng = new PM_PRNG();
		int testNextInt = rng.nextInt();  // 16807
		int testNextInt2 = rng.nextInt();	// 282475249	
		double testNextDouble = rng.nextDouble(); // 0.755604293083588
		
		
		Debug.Log ( testNextInt == 16807 );
		Debug.Log ( testNextInt2 == 282475249 );
		Debug.Log ( testNextDouble );
		Debug.Log ( testNextDouble.ToString() == "0.755604293083588"  );
		
	}

}
