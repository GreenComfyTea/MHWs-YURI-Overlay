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
		get => this.vector;
		set
		{
			this.vector = value;
			this.UpdateFromVector(value);
		}
	}

	public uint Rgba
	{
		get => this._rgba;
		set
		{
			this._rgba = value;
			this.UpdateFromRgba(value);
		}
	}

	public uint Abgr
	{
		get => this._abgr;
		set
		{
			this._abgr = value;
			this.UpdateFromAbgr(value);
		}
	}

	public string RgbaHex
	{
		get => this._rgbaHex;
		set
		{
			this._rgbaHex = value;
			this.UpdateFromRgbaHex(value);
		}
	}

	public string AbgrHex
	{
		get => this._abgrHex;
		set
		{
			this._abgrHex = value;
			this.UpdateFromAbgrHex(value);
		}
	}

	public ColorInfo()
	{
		this.Vector = new Vector4(0f, 0f, 0f, 1f);
	}

	public ColorInfo(Vector4 vector)
	{
		this.Vector = vector;
	}

	private void UpdateFromVector(Vector4 newVector)
	{
		var red = (byte) (newVector.X * 255f);
		var green = (byte) (newVector.Y * 255f);
		var blue = (byte) (newVector.Z * 255f);
		var alpha = (byte) (newVector.W * 255f);

		this._rgba = ((uint) red << 24) | ((uint) green << 16) | ((uint) blue << 8) | alpha;
		this._abgr = ((uint) alpha << 24) | ((uint) blue << 16) | ((uint) green << 8) | red;
		this._rgbaHex = $"#{this._rgba:X8}";
		this._abgrHex = $"#{this._abgr:X8}";
	}

	private void UpdateFromRgba(uint rgba)
	{
		var red = (byte) (rgba >> 24);
		var green = (byte) (rgba >> 16);
		var blue = (byte) (rgba >> 8);
		var alpha = (byte) rgba;

		this.vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		this._abgr = ((uint) alpha << 24) | ((uint) blue << 16) | ((uint) green << 8) | red;
		this._rgbaHex = $"#{rgba:X8}";
		this._abgrHex = $"#{this._abgr:X8}";
	}

	private void UpdateFromAbgr(uint abgr)
	{
		var red = (byte) abgr;
		var green = (byte) (abgr >> 8);
		var blue = (byte) (abgr >> 16);
		var alpha = (byte) (abgr >> 24);

		this.vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		this._rgba = ((uint) red << 24) | ((uint) green << 16) | ((uint) blue << 8) | alpha;
		this._rgbaHex = $"#{this._rgba:X8}";
		this._abgrHex = $"#{abgr:X8}";
	}

	private void UpdateFromRgbaHex(string rgbaHex)
	{
		if(rgbaHex.Length != 9)
		{
			return;
		}

		this._rgba = uint.Parse(rgbaHex[1..], NumberStyles.HexNumber);

		var red = (byte) (this.Rgba >> 24);
		var green = (byte) (this.Rgba >> 16);
		var blue = (byte) (this.Rgba >> 8);
		var alpha = (byte) this.Rgba;

		this.vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		this._abgr = ((uint) alpha << 24) | ((uint) blue << 16) | ((uint) green << 8) | red;
		this._abgrHex = $"#{this._abgr:X8}";
	}

	private void UpdateFromAbgrHex(string abgrHex)
	{
		if(abgrHex.Length != 9)
		{
			return;
		}

		this._abgr = uint.Parse(abgrHex[1..], NumberStyles.HexNumber);

		var red = (byte) this.Abgr;
		var green = (byte) (this.Abgr >> 8);
		var blue = (byte) (this.Abgr >> 16);
		var alpha = (byte) (this.Abgr >> 24);

		this.vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		this._rgba = ((uint) red << 24) | ((uint) green << 16) | ((uint) blue << 8) | alpha;
		this._rgbaHex = $"#{this._rgba:X8}";
	}

	public override string ToString()
	{
		return $"Vector4: {this.vector} | rgba: {this._rgba} | abgr: {this._abgr} | rgbaHex: {this._rgbaHex} | abgrHex: {this._abgrHex}";
	}
}