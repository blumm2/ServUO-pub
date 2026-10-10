using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Server.Engines.Reports
{
	// Minimal replacement for System.Web.UI.HtmlTextWriter (WebForms), which does not exist on modern
	// .NET. Covers exactly what HtmlRenderer uses: attributes are queued, then written with the next
	// begin tag (HTML-encoded, like WebForms); void elements (img, link) are self-closed.
	public enum HtmlTextWriterTag
	{
		A,
		Body,
		Center,
		Head,
		Html,
		Img,
		Link,
		Table,
		Td,
		Title,
		Tr
	}

	public enum HtmlTextWriterAttribute
	{
		Align,
		Border,
		Cellpadding,
		Cellspacing,
		Class,
		Colspan,
		Height,
		Href,
		Onclick,
		Src,
		Type,
		Width
	}

	public class HtmlTextWriter : IDisposable
	{
		private readonly TextWriter m_Writer;
		private readonly string m_Indent;
		private readonly List<KeyValuePair<string, string>> m_Attributes = new List<KeyValuePair<string, string>>();
		private readonly Stack<string> m_Open = new Stack<string>();

		public HtmlTextWriter(TextWriter writer, string tabString)
		{
			m_Writer = writer;
			m_Indent = tabString ?? "\t";
		}

		public void AddAttribute(HtmlTextWriterAttribute attribute, string value)
		{
			AddAttribute(attribute.ToString().ToLowerInvariant(), value);
		}

		public void AddAttribute(string name, string value)
		{
			m_Attributes.Add(new KeyValuePair<string, string>(name, value));
		}

		public void RenderBeginTag(HtmlTextWriterTag tag)
		{
			string name = tag.ToString().ToLowerInvariant();

			WriteIndent(m_Open.Count);
			m_Writer.Write('<');
			m_Writer.Write(name);

			foreach (var attr in m_Attributes)
			{
				m_Writer.Write(' ');
				m_Writer.Write(attr.Key);
				m_Writer.Write("=\"");
				m_Writer.Write(WebUtility.HtmlEncode(attr.Value));
				m_Writer.Write('"');
			}

			m_Attributes.Clear();

			if (tag == HtmlTextWriterTag.Img || tag == HtmlTextWriterTag.Link)
			{
				// Void element: closed here; the matching RenderEndTag writes nothing.
				m_Writer.WriteLine(" />");
				m_Open.Push(null);
			}
			else
			{
				m_Writer.WriteLine('>');
				m_Open.Push(name);
			}
		}

		public void RenderEndTag()
		{
			string name = m_Open.Pop();

			if (name == null)
			{
				return;
			}

			WriteIndent(m_Open.Count);
			m_Writer.Write("</");
			m_Writer.Write(name);
			m_Writer.WriteLine('>');
		}

		public void Write(string text)
		{
			m_Writer.Write(text);
		}

		public void Write(string format, params object[] args)
		{
			m_Writer.Write(format, args);
		}

		public void Dispose()
		{
			m_Writer.Flush();
		}

		private void WriteIndent(int depth)
		{
			for (int i = 0; i < depth; i++)
			{
				m_Writer.Write(m_Indent);
			}
		}
	}
}
