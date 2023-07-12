using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEndApi.Utilities
{
	// Native/JsonFileUtils.cs
	public static class NativeJsonFileUtils {
		private static readonly JsonSerializerOptions _options =
		    new () { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

		public static void SimpleWrite (object obj, string fileName)
		{
			var jsonString = System.Text.Json.JsonSerializer.Serialize (obj, _options);
			File.WriteAllText (fileName, jsonString);
		}

		/// <summary>
		/// For better readable output file.
		/// <b>Note:</b> We should avoid pretty printing for the production code.
		/// The fact is it generates a lot of extra white spaces that affect the performance and the bandwidth.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fileName"></param>
		public static void PrettyWrite (object obj, string fileName)
		{
			var options = new JsonSerializerOptions (_options) {
				WriteIndented = true
			};
			var jsonString = System.Text.Json.JsonSerializer.Serialize (obj, options);
			File.WriteAllText (fileName, jsonString);
		}

		/// <summary>
		/// Serialize to UTF-8 bytes instead, which is about 5-10% faster than using the string-based methods.
		/// The difference is because we don’t need to convert bytes (as UTF-8) to strings (UTF-16).
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fileName"></param>
		public static void Utf8BytesWrite (object obj, string fileName)
		{
			var utf8Bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes (obj, _options);
			File.WriteAllBytes (fileName, utf8Bytes);
		}

		/// <summary>
		/// Write directly to FileStream.
		/// <b>Note:</b> We don’t want to eat up the memory holding the full output in a local copy.
		/// It can be disastrous if we have a large object graph.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fileName"></param>
		public static void StreamWrite (object obj, string fileName)
		{
			using var fileStream = File.OpenWrite (fileName);
			using var utf8JsonWriter = new Utf8JsonWriter (fileStream);
			System.Text.Json.JsonSerializer.Serialize (utf8JsonWriter, obj, _options);
		}

		/// <summary>
		/// Write to file Async.
		/// <b>Note:</b> We should not block the running thread keeping it waiting for an expensive I/O operation to complete.
		/// While this does not matter for the performance of the JSON-write operation itself
		/// , it does matter for the responsiveness and/or scalability of the caller program.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static async Task StreamWriteAsync (object obj, string fileName, CancellationToken cancellationToken)
		{
			await using var fileStream = new FileStream (fileName, FileMode.Append, FileAccess.Write);
			await System.Text.Json.JsonSerializer.SerializeAsync (fileStream, obj, _options, cancellationToken);
			using (var streamWriter = new StreamWriter (fileStream)) {
				await streamWriter.WriteAsync (Environment.NewLine);
			}
		}

		public static void WriteDynamicJsonObject (JsonObject jsonObj, string fileName)
		{
			using var fileStream = File.OpenWrite (fileName);
			using var utf8JsonWriter = new Utf8JsonWriter (fileStream);
			jsonObj.WriteTo (utf8JsonWriter);
		}
	}

	// Newtonsoft/JsonFileUtils.cs
	public static class NewtonsoftJsonFileUtils {
		private static readonly JsonSerializerSettings _options
		    = new () { NullValueHandling = NullValueHandling.Ignore };

		public static void SimpleWrite (object obj, string fileName)
		{
			var jsonString = JsonConvert.SerializeObject (obj, _options);
			File.WriteAllText (fileName, jsonString);
		}

		/// <summary>
		/// For better readable output file.
		/// <b>Note:</b> We should avoid pretty printing for the production code.
		/// The fact is it generates a lot of extra white spaces that affect the performance and the bandwidth.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fileName"></param>
		public static void PrettyWrite (object obj, string fileName)
		{
			var jsonString = JsonConvert.SerializeObject (obj, Formatting.Indented, _options);
			File.WriteAllText (fileName, jsonString);
		}

		/// <summary>
		/// Serialize to UTF-8 bytes instead, which is about 5-10% faster than using the string-based methods.
		/// The difference is because we don’t need to convert bytes (as UTF-8) to strings (UTF-16).
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		static byte [] SerializeToUtf8Bytes (object obj, JsonSerializerSettings options)
		{
			using var stream = new MemoryStream ();
			using var streamWriter = new StreamWriter (stream);
			using var jsonWriter = new JsonTextWriter (streamWriter);
			Newtonsoft.Json.JsonSerializer.CreateDefault (options).Serialize (jsonWriter, obj);
			jsonWriter.Flush ();
			stream.Position = 0;
			return stream.ToArray ();
		}
		public static void Utf8BytesWrite (object obj, string fileName)
		{
			var utf8Bytes = SerializeToUtf8Bytes (obj, _options);
			File.WriteAllBytes (fileName, utf8Bytes);
		}

		/// <summary>
		/// Write directly to FileStream.
		/// <b>Note:</b> We don’t want to eat up the memory holding the full output in a local copy.
		/// It can be disastrous if we have a large object graph.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fileName"></param>
		public static void StreamWrite (object obj, string fileName)
		{
			using var streamWriter = File.CreateText (fileName);
			using var jsonWriter = new JsonTextWriter (streamWriter);
			Newtonsoft.Json.JsonSerializer.CreateDefault (_options).Serialize (jsonWriter, obj);
		}

		/// <summary>
		/// Unfortunately, NewtonsoftJson doesn’t support asynchronous serialization out of the box yet.
		/// What we can do at best is offload this I/O operation to thread-pool.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static async Task StreamWriteAsync (object obj, string fileName, CancellationToken cancellationToken)
		{
			await Task.Run (() => StreamWrite (obj, fileName), cancellationToken);
		}

		public static void WriteDynamicJsonObject (JObject jsonObj, string fileName)
		{
			using var streamWriter = File.CreateText (fileName);
			using var jsonWriter = new JsonTextWriter (streamWriter);
			jsonObj.WriteTo (jsonWriter);
		}
	}
}

