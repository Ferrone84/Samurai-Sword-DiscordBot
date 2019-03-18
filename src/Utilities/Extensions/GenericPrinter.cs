﻿using System;
using System.Collections;
using System.Text;

namespace KatanaBot
{
	public static class GenericPrinter
	{
		private static string Inspect(this object o) { return o.ToString(); }
		private static string Inspect(this ValueType o) { return o.ToString(); }
		private static string Inspect(this string o) { return o.ToString(); }
		private static string Inspect(this Type t)
		{
			var sb = new StringBuilder();
			sb.Append(t.Name);
			if (t.IsGenericType) {
				sb.Append(ValuesInspect(t.GenericTypeArguments, '<', '>'));
			}
			return sb.ToString();
		}
		private static string Inspect(this IEnumerable o)
		{
			var sb = new StringBuilder();
			sb.Append($"[{Inspect(o.GetType())}] ");
			sb.Append(o.ValuesInspect());
			return sb.ToString();
		}
		private static string ValuesInspect(this IEnumerable o, char start = '{', char stop = '}', string sep = ", ")
		{
			var sb = new StringBuilder();
			sb.Append(start);
			bool first = true;
			foreach (var e in o) {
				if (first) { first = false; }
				else { sb.Append(sep); }
				sb.Append(Inspect((dynamic)e));
			}
			sb.Append(stop);
			return sb.ToString();
		}
		private static string Inspect(this IDictionary o)
		{
			var sb = new StringBuilder();
			sb.Append($"[{Inspect(o.GetType())}] ");
			sb.Append(o.ValuesInspect());
			return sb.ToString();
		}
		private static string ValuesInspect(this IDictionary o)
		{
			var sb = new StringBuilder();
			sb.Append('{');
			var e = o.GetEnumerator();
			bool first = true;
			while (e.MoveNext()) {
				if (first) { first = false; }
				else { sb.Append(", "); }
				sb.Append('{');
				sb.Append(Inspect((dynamic)e.Key));
				sb.Append(" -> ");
				sb.Append(Inspect((dynamic)e.Value));
				sb.Append('}');
			}
			sb.Append('}');
			return sb.ToString();
		}

		public static string GetStr(this object o)
		{
			if (IsNull(o)) { return "NULL"; }
			return Inspect((dynamic)o);
		}

		public static void Print(this object o)
		{
			if (IsNull(o)) { return; }
			Console.Write(Inspect((dynamic)o));
		}

		public static void Println(this object o)
		{
			if (IsNull(o)) { return; }
			Console.WriteLine(Inspect((dynamic)o));
		}

		public static void Debug(this object o, string before = "/", string after = "/")
		{
			if (IsNull(o)) { return; }
			Console.WriteLine(before + Inspect((dynamic)o) + after);
		}

		private static bool IsNull(object o)
		{
			if (o == null) {
				Console.WriteLine("NULL");
				return true;
			}
			return false;
		}
	}
}
