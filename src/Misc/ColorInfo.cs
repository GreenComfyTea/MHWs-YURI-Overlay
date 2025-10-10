using System.Globalization;
using System.Numerics;

namespace YURI_Overlay;

internal sealed class ColorInfo
{
	public Vector4 vector;
	private uint _rgba = 0x000000FF;
	private uint _abgr = 0XFF000000;
	private string _rgbaHex = "0x000000FF";
	private string _abgrHex = "0xFF000000";

	public Vector4 Vector
	{
		get => vector;
		set
		{
			vector = value;
			UpdateFromVector(value);
		}
	}

	public uint Rgba
	{
		get => _rgba;
		set
		{
			_rgba = value;
			UpdateFromRgba(value);
		}
	}

	public uint Abgr
	{
		get => _abgr;
		set
		{
			_abgr = value;
			UpdateFromAbgr(value);
		}
	}

	public string RgbaHex
	{
		get => _rgbaHex;
		set
		{
			_rgbaHex = value;
			UpdateFromRgbaHex(value);
		}
	}

	public string AbgrHex
	{
		get => _abgrHex;
		set
		{
			_abgrHex = value;
			UpdateFromAbgrHex(value);
		}
	}

	public ColorInfo()
	{
		Vector = new Vector4(0f, 0f, 0f, 1f);
	}

	public ColorInfo(Vector4 vector)
	{
		Vector = vector;
	}

	private void UpdateFromVector(Vector4 newVector)
	{
		var red = (byte) (newVector.X * 255f);
		var green = (byte) (newVector.Y * 255f);
		var blue = (byte) (newVector.Z * 255f);
		var alpha = (byte) (newVector.W * 255f);

		_rgba = ((uint) red << 24) | ((uint) green << 16) | ((uint) blue << 8) | alpha;
		_abgr = ((uint) alpha << 24) | ((uint) blue << 16) | ((uint) green << 8) | red;
		_rgbaHex = $"#{_rgba:X8}";
		_abgrHex = $"#{_abgr:X8}";
	}

	private void UpdateFromRgba(uint rgba)
	{
		var red = (byte) (rgba >> 24);
		var green = (byte) (rgba >> 16);
		var blue = (byte) (rgba >> 8);
		var alpha = (byte) rgba;

		vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		_abgr = ((uint) alpha << 24) | ((uint) blue << 16) | ((uint) green << 8) | red;
		_rgbaHex = $"#{rgba:X8}";
		_abgrHex = $"#{_abgr:X8}";
	}

	private void UpdateFromAbgr(uint abgr)
	{
		var red = (byte) abgr;
		var green = (byte) (abgr >> 8);
		var blue = (byte) (abgr >> 16);
		var alpha = (byte) (abgr >> 24);

		vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		_rgba = ((uint) red << 24) | ((uint) green << 16) | ((uint) blue << 8) | alpha;
		_rgbaHex = $"#{_rgba:X8}";
		_abgrHex = $"#{abgr:X8}";
	}

	private void UpdateFromRgbaHex(string rgbaHex)
	{
		if(rgbaHex.Length != 9) return;

		_rgba = uint.Parse(rgbaHex[1..], NumberStyles.HexNumber);

		var red = (byte) (Rgba >> 24);
		var green = (byte) (Rgba >> 16);
		var blue = (byte) (Rgba >> 8);
		var alpha = (byte) Rgba;

		vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		_abgr = ((uint) alpha << 24) | ((uint) blue << 16) | ((uint) green << 8) | red;
		_abgrHex = $"#{_abgr:X8}";
	}

	private void UpdateFromAbgrHex(string abgrHex)
	{
		if(abgrHex.Length != 9) return;

		_abgr = uint.Parse(abgrHex[1..], NumberStyles.HexNumber);

		var red = (byte) Abgr;
		var green = (byte) (Abgr >> 8);
		var blue = (byte) (Abgr >> 16);
		var alpha = (byte) (Abgr >> 24);

		vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		_rgba = ((uint) red << 24) | ((uint) green << 16) | ((uint) blue << 8) | alpha;
		_rgbaHex = $"#{_rgba:X8}";
	}

	public override string ToString()
	{
		return $"Vector4: {vector} | rgba: {_rgba} | abgr: {_abgr} | rgbaHex: {_rgbaHex} | abgrHex: {_abgrHex}";
	}
}