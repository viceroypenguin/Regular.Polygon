using System.Threading.Channels;

namespace Regular.Polygon.SocketManager;

internal sealed partial class PolygonSocketManager
{
	private interface IChannelContainer
	{
		Channel<JsonElement>? Channel { get; }
	}
}
