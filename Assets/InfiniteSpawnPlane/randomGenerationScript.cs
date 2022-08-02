using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class randomGenerationScript : MonoBehaviour {
	[Header("Player Game Object")]
	// playerToFollow: The player game object in your scene that the spawning system will follow
	[Tooltip("Drag and drop your in-scene player game object here; this will allow the spawner to follow your player!")]
	public GameObject playerToFollow;

	[Header("Spawn Controls")]
	// aiPrefabsToSpawn: Add your finished NPC Prefabs to this array
	[Tooltip("Ensure you make each of the arrays below the same size. For example, if you have 2 enemy prefabs you want to spawn make sure there are 2 spawn timers.")]
	public GameObject[] aiPrefabsToSpawn;

	// spawnTimer: How long until the next spawn; each element in this array should correspond to the same element in aiPrefabsToSpawn.
	[Tooltip("Set the time for the period in seconds, where the period is the time between each random spawn. Ensure that the Size value of this array matches the Size value of Prefabs To Spawn.")]
	public float[] spawnTimer;

	// spawnCeiling: How far UP the spawns can go. I.e., reducing this means any spawns will be limited to being under this value in the world Y-Axis.
	[Tooltip("Set the 'Ceiling' for spawns; they won't spawn above this value in the world Y-Axis.")]
	public float[] spawnCeiling;

	// spawnCeiling: How far DOWN the spawns can go. I.e., reducing this means any spawns will be limited to being above this value in the world Y-Axis.
	[Tooltip("Set the 'Floor' for spawns; they won't spawn below this value in the world Y-Axis.")]
	public float[] spawnFloor;

	[Header("Spawn Parameters")]
	[Tooltip("Set how high off the ground the instantiated prefab will be created. Important if you find your prefabs halfway in the ground. Must be same size as the Spawn Controls arrays. Allows varying sized spawns.")]
	public float[] InstantiateHeight;

	[Header("Time of Previous Spawn (Debugging Aid)")]
	// lastSpawnTimer: Stores the last time a prefab has spawned, to keep track.
	[Tooltip("Use this to see when the last object spawned. Great for ensuring that you are at least spawning something (debugging).")]
	public float[] lastSpawnTimer;

	// playerFollowSpeed: This sets the speed at which the spawning system will follow the player around the open world.
	[HideInInspector]
	public float playerFollowSpeed = 50;

	// The length of the ray from its origin to the end. Large value effectively assures that the ray hits the ground despite player height.
	float lengthOfRaycast = 9999;

	// spawnObjectMoveTowardsVector: Used to help the spawner follow the player.
	Vector3 spawnObjectMoveTowardsVector;

	// raycastOriginPoint: This vector is manipulated by the getRandomLocation method.
	// This is the 'Source' of a Raycast downward from the plane
	Vector3 raycastOriginPoint;

	// The first raycast hit/didn't hit a spawn layer.
	bool spawnIsAGo;
	// The second raycast hit/didn't hit a no-spawn layer.
	bool spawnNoGo;

	//TODO: Remove this?
	float timerFunc;

	[Header("Layer Masks")]
	// Set the layer mask for the Raycasting (Preferably terrain itself).
	[Tooltip("This layer mask should be set to the layer of the terrain; this is used by the Raycasting.")]
	public LayerMask layerMaskRaycast;
	// Set the layer mask for things we DON'T want to spawn at.
	[Tooltip("This layer mask should be set to the layer of the obstacles in your environment; this is used to prevent the spawner from spawning something inside a rock for example.")]
	public LayerMask layerMaskNoSpawn;

	[Header("Player Buffer Zone")]
	[Tooltip("Enter a float value, prevents spawns from occurring within this distance around the player.")]
	public float setDistance;


	// steepnessBufferAngle: Represents the angle between the up vector (points towards sky) and the normal of the terrain, that is allowed.
	[Header("Terrain Steepness Buffer")]
	[Tooltip("This controls how steep terrain can be before the spawner views it as unsuitable. this value is from 0.0 to 1.0 where 0.1 is extremely steep and 1.0 is flat, and corresponds to the Y-Rotation Axis of the Terrain Normal.")]
	[Range(0.0f, 1.0f)]
	public float steepnessBufferAngle;

	// Spawn plane represents the area where things can spawn. 
	// Scale it up/down in order to increase/decrease the area around the player that something can spawn
	// If you change the name of the game object, be sure to update the name below so spawnPlane can find it
	private GameObject spawnPlane;


	// Use this for initialization
	void Start () { 

		timerFunc = 0;

		// Initialize the last spawn timer array.
		for (int arrayIndex = 0; arrayIndex < lastSpawnTimer.Length; arrayIndex++) 
			lastSpawnTimer[arrayIndex] = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		// ========================= FOLLOW THE PLAYER ========================= //
		// Controls the movement of randomGenerationObj, the plane that has this script attached to it. This keeps it above the player at all times.
		// The value 60 below controls the height above the player, and the players X/Z axis determines the spawners X/Z axes.
		spawnObjectMoveTowardsVector = new Vector3 (playerToFollow.transform.position.x, playerToFollow.transform.position.y + 120, playerToFollow.transform.position.z);
		transform.position = Vector3.MoveTowards (transform.position, spawnObjectMoveTowardsVector, playerFollowSpeed * Time.deltaTime);


		// ========================= MAIN LOOP ========================= //
		// Goes through the array of objects that you set in the Inspector window, and handles each one.
		for (int arrayIndex = 0; arrayIndex < aiPrefabsToSpawn.Length; arrayIndex++) {
			// Assign the 'Spawner' a random location within the Spawn-Plane.

			if (timerFunc == 10) { // Reduce the number of times we calculate a random location.
				raycastOriginPoint = getRandomLocation ();
				timerFunc = 0;
			} else
				timerFunc++;
			
			// Cast the ray downward from this random location to get information from the impact.
			Vector3 raycastDirection = transform.TransformDirection (Vector3.down);
			// We need the RaycastHit point to get information about where the raycast hit the terrain.
			RaycastHit impactPoint;
			// Check for a no-spawn layer impact.

			// ============================== SPAWN GO/NO-GO ============================== //
			if (Physics.Raycast (raycastOriginPoint, raycastDirection, out impactPoint, lengthOfRaycast, layerMaskNoSpawn)) {
				spawnNoGo = true;
			} else
				spawnNoGo = false;
			// Check for a yes-spawn layer impact. Order of these two is important; this second raycast is the one with important spawn information.
			if (Physics.Raycast (raycastOriginPoint, raycastDirection, out impactPoint, lengthOfRaycast, layerMaskRaycast))
				spawnIsAGo = true;
			else {
				spawnIsAGo = false;
			}

			// If the point calculated is above the ceiling, OR below the floor, then wait to spawn until next calculation.
			if ((spawnCeiling [arrayIndex] < impactPoint.point.y) || (spawnFloor [arrayIndex] > impactPoint.point.y)) {
				spawnIsAGo = false;
			}

			// If the point calculated is within the buffer zone (no spawn near player), then wait to spawn until next calculation.
			if (playerBufferArea (playerToFollow.transform.position, impactPoint.point, setDistance) == false) {
				spawnIsAGo = false;
			}

			// If the terrain normal angle is less than the buffer set in the inspector, don't spawn there (0.0 to 1.0, steep to flat.)
			if (terrainBufferAngle (impactPoint.normal, steepnessBufferAngle) == false) {
				spawnIsAGo = false;
			}

			// ============================== INITIATE SPAWN ============================== //
			// If there is no obstacle/no-spawn area, and we struck terrain, start spawn.
			if (spawnIsAGo & (!spawnNoGo)) {
				// At this point we are sure we have a good spot to spawn something.
				if ((Time.time - lastSpawnTimer [arrayIndex]) > spawnTimer [arrayIndex]) {
					// At this point we are sure its time to spawn the next prefab for this element in the array
					Instantiate(aiPrefabsToSpawn[arrayIndex], new Vector3 (impactPoint.point.x, impactPoint.point.y + InstantiateHeight[arrayIndex], impactPoint.point.z), Quaternion.identity);
					lastSpawnTimer[arrayIndex] = Time.time; // Reset the clock for the next spawn
				}


			} 
		}
	}

	// This function gets a random location in the spawn plane.
	Vector3 getRandomLocation()
	{
		// Generate random coordinates within this plane:
		// The position of the plane is the center point within the plane; this is where the player is!
		// The local scale of the plan is the length of the plane along one side, like the side of a square.
		// spawnBoundary moves the random range away from the center point; since the player is here, this means we created a buffer away from the player for the range.
		float randomX = Random.Range (gameObject.transform.position.x - (gameObject.transform.localScale.x * 4), gameObject.transform.position.x + (gameObject.transform.localScale.x * 4)); 
		float unchangedY = gameObject.transform.position.y; // No need to change this for the spawner, its a plane so its only 2 Dimensional
		float randomZ = Random.Range (gameObject.transform.position.z - (gameObject.transform.localScale.z * 4), gameObject.transform.position.z + (gameObject.transform.localScale.z * 4)); 

		// We now create a vector from the three positions so that we can give our raycast an origin point!
		Vector3 randomPosition = new Vector3 (randomX, unchangedY, randomZ);

		Vector3 originForDebugRay = new Vector3 (transform.position.x, transform.position.y + 10, transform.position.z);
		Debug.DrawLine(originForDebugRay, randomPosition);

		return randomPosition;
	}

	bool playerBufferArea(Vector3 playerPosition, Vector3 impactPoint, float setDistance){
		// Calculate the distance between the player and the spawn point.
		// If they are too close, return false.

		float distanceToSpawn = Vector3.Distance (playerPosition, impactPoint);

		// If the distance to the spawn point is greater than the distance set in the inspector, return true.
		if (distanceToSpawn > setDistance) {
			return true;
		} else {
			return false;
		}
	}

	bool terrainBufferAngle(Vector3 terrainNormal, float bufferAngle){
		// This function analyzes the normal vector of the terrain to see if it is steep or not.
		if (bufferAngle <= terrainNormal.y) {
			return true;
		} else{
			return false;
		}
	}


}

