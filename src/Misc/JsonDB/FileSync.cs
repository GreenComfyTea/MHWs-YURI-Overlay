using System.Text;

namespace YURI_Overlay;

internal sealed class FileSync
{
	public readonly string pathFileName;

	public FileSync(string pathFileName)
	{
		this.pathFileName = pathFileName;
	}

	public string Read()
	{
		return File.Exists(this.pathFileName) ? this.ReadFromFile() : Constants.EMPTY_JSON;
	}

	public bool Write(string json)
	{
		return this.WriteToFile(json);
	}

	public void Delete()
	{
		try
		{
			File.Delete(this.pathFileName);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private string ReadFromFile()
	{
		try
		{
			using var file = File.Open(this.pathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			using StreamReader streamReader = new(file);
			var content = streamReader.ReadToEnd();

			return content;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);

			return Constants.EMPTY_JSON;
		}
	}

	private bool WriteToFile(string json)
	{
		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(this.pathFileName)!);

			using var file = File.Open(this.pathFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

			using StreamWriter streamWriter = new(file, Encoding.UTF8);
			streamWriter.AutoFlush = true;

			file.SetLength(0);

			streamWriter.Write(json);

			return true;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);

			return false;
		}
	}
}