using System.Text;

namespace YURI_Overlay;

internal class FileSync
{
	public string PathFileName;

	public FileSync(string pathFileName)
	{
		PathFileName = pathFileName;
	}

	public string Read()
	{
		return File.Exists(PathFileName) ? ReadFromFile() : Constants.EmptyJson;
	}

	public bool Write(string json)
	{
		return WriteToFile(json);
	}

	public void Delete()
	{
		try
		{
			File.Delete(PathFileName);
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
			var file = File.Open(PathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			StreamReader streamReader = new(file);
			var content = streamReader.ReadToEnd();

			streamReader.Close();
			streamReader.Dispose();

			return content;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return Constants.EmptyJson;
		}
	}

	private bool WriteToFile(string json)
	{
		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(PathFileName)!);
			var file = File.Open(PathFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

			StreamWriter streamWriter = new(file, Encoding.UTF8);
			streamWriter.AutoFlush = true;

			file.SetLength(0);

			streamWriter.Write(json);
			streamWriter.Close();

			return true;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return false;
		}
	}
}