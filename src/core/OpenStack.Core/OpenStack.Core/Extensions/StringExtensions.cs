﻿using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace OpenStack.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToJavaScriptString(this string value, bool addDoubleQuotes = false)
        {
            if (string.IsNullOrEmpty(value))
                return addDoubleQuotes ? "\"\"" : string.Empty;

            int len = value.Length;
            bool needEncode = false;
            char c;
            for (int i = 0; i < len; i++)
            {
                c = value[i];

                if (c >= 0 && c <= 31 || c == 34 || c == 39 || c == 60 || c == 62 || c == 92)
                {
                    needEncode = true;
                    break;
                }
            }

            if (!needEncode)
                return addDoubleQuotes ? "\"" + value + "\"" : value;

            var sb = new System.Text.StringBuilder();
            if (addDoubleQuotes)
                sb.Append('"');

            for (int i = 0; i < len; i++)
            {
                c = value[i];
                if (c >= 0 && c <= 7 || c == 11 || c >= 14 && c <= 31 || c == 39 || c == 60 || c == 62)
                    sb.AppendFormat("\\u{0:x4}", (int)c);
                else switch ((int)c)
                    {
                        case 8:
                            sb.Append("\\b");
                            break;

                        case 9:
                            sb.Append("\\t");
                            break;

                        case 10:
                            sb.Append("\\n");
                            break;

                        case 12:
                            sb.Append("\\f");
                            break;

                        case 13:
                            sb.Append("\\r");
                            break;

                        case 34:
                            sb.Append("\\\"");
                            break;

                        case 92:
                            sb.Append("\\\\");
                            break;

                        default:
                            sb.Append(c);
                            break;
                    }
            }

            if (addDoubleQuotes)
                sb.Append('"');

            return sb.ToString();
        }

        public static string ToFormattableJsonString(this string value)
        {
            value = value.Replace("{", "{{");
            value = value.Replace("}", "}}");
            value = value.Replace("\"{{", "\"{");
            value = value.Replace("}}\"", "}\"");

            return value;
        }

        public static string ToJsonString(this JsonDocument jsonDocument)
        {
            string jsonString;

            using (var stream = new MemoryStream())
            {
                Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                jsonDocument.WriteTo(writer);
                writer.Flush();
                jsonString = Encoding.UTF8.GetString(stream.ToArray());
            }

            return jsonString;
        }
    }
}
