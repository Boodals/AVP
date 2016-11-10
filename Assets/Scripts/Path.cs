using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
	[System.Serializable]
	private class PathNode
	{
		[SerializeField]
		public Vector3 position;

		public float DistTo(PathNode otherNode)
		{
			return Vector3.Distance(position, otherNode.position);
		}
		public float DistTo(Vector3 pos)
		{
			return Vector3.Distance(position, pos);
		}
	}

	private class PathSegment
	{
		public PathNode[] nodes = new PathNode[4];

		private float _length;

		public float length
		{
			get
			{
				return _length;
			}
		}

		public PathSegment(PathNode start, PathNode startLine, PathNode endLine, PathNode end)
		{
			nodes[0] = start;
			nodes[1] = startLine;
			nodes[2] = endLine;
			nodes[3] = end;

			_length = start.DistTo(end); //TODO: make this actual distance along curve, somehow.
		}


		public Vector3 GetPos(float dist)
		{
			float t = dist / _length;

			Vector3 a = nodes[0].position;
			Vector3 b = nodes[1].position;
			Vector3 c = nodes[2].position;
			Vector3 d = nodes[3].position;

			Vector3 ab = Vector3.Lerp(a, b, t);
			Vector3 bc = Vector3.Lerp(b, c, t);
			Vector3 cd = Vector3.Lerp(c, d, t);

			Vector3 abc = Vector3.Lerp(ab, bc, t);
			Vector3 bcd = Vector3.Lerp(bc, cd, t);

			return Vector3.Lerp(abc, bcd, t);
		}
	}

	[SerializeField]
	private List<PathNode> nodes = new List<PathNode>(4);
	
	private List<PathSegment> segments = new List<PathSegment>();

	private float totalDistance = 0f;


	void OnValidate()
	{
		while(nodes.Count < 4)
		{
			nodes.Add(new PathNode());
		}
	}

	void Start()
	{
		AssignSegments();
		CalculateTotalDistance();
	}

	public Vector3 GetPos(float dist)
	{
		foreach(PathSegment segment in segments)
		{
			if(dist >= segment.length)
			{
				dist -= segment.length;
				continue;
			}

			return segment.GetPos(dist);
		}

		PathSegment last = segments[segments.Count - 1];
		return last.GetPos(last.length);
	}


	void OnDrawGizmosSelected()
	{
		//Inefficient, but itll do
		AssignSegments();
		CalculateTotalDistance();

		const float res = 0.1f;

		Vector3 lastPos = GetPos(0f);
		for(float d = res; d <= totalDistance; d += res)
		{
			Vector3 pos = GetPos(d);
			Gizmos.DrawLine(lastPos, pos);

			//Debug.Log(pos);

			lastPos = pos;
		}

		for (int i = 1; i < nodes.Count; i += 3)
		{
			Gizmos.color = Color.gray;
			Gizmos.DrawLine(nodes[i - 1].position, nodes[i + 0].position);
			Gizmos.DrawLine(nodes[i + 1].position, nodes[i + 2].position);
			Gizmos.color = Color.white;
		}
	}


	private void CalculateTotalDistance()
	{
		//Sum up the total distance
		totalDistance = 0f;
		foreach (PathSegment segment in segments)
		{
			totalDistance += segment.length;
		}
	}

	private void AssignSegments()
	{
		segments = new List<PathSegment>();

		for(int i = 1; i < nodes.Count; i += 3)
		{
			//Each segment shares its first node with the previous segments last node
			segments.Add(new PathSegment(nodes[i - 1], nodes[i + 0], nodes[i + 1], nodes[i + 2]));
		}
	}

}
