﻿using UnityEngine;

namespace Akela.Tools
{
	public static class Vector3Extensions
	{
		public static Vector2 ToTopviewVector2(this Vector3 source)
		{
			return new Vector2(source.x, source.z);
		}

		public static Vector3 ToTopviewVector3(this Vector3 source)
		{
			return new Vector3(source.x, 0f, source.z);
		}

		public static bool IsBetween(this Vector3 c, Vector3 a, Vector3 b)
		{
			return c.IsBetween(a, b, b - a);
		}

		public static bool IsBetween(this Vector3 c, Vector3 a, Vector3 b, Vector3 projectionVector)
		{
			c = Vector3.Project(c, projectionVector);
			a = Vector3.Project(a, projectionVector);
			b = Vector3.Project(b, projectionVector);

			return Vector3.Dot((b - a).normalized, (c - b).normalized) < 0f && Vector3.Dot((a - b).normalized, (c - a).normalized) < 0f;
		}

		public static Vector3 Inverse(this Vector3 v)
		{
			return new(1f / v.x, 1f / v.y, 1f / v.z);
		}

		public static Vector3 Mask(this Vector3 v, Vector3 mask)
		{
			return new(
				mask.x == 0f ? 0f : v.x,
				mask.y == 0f ? 0f : v.y,
				mask.z == 0f ? 0f : v.z
			);
		}

		public static Vector3 InvertMask(this Vector3 v)
		{
			return new(
				v.x == 0f ? 1f : 0f,
				v.y == 0f ? 1f : 0f,
				v.z == 0f ? 1f : 0f
			);
		}

		public static Vector3 ComponentMask(this Vector3 v, float mask, float fallback = 0f)
		{
			return new(
				v.x == mask ? v.x : fallback,
				v.y == mask ? v.y : fallback,
				v.z == mask ? v.z : fallback
			);
		}
	}
}