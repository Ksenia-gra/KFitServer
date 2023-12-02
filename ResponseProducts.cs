using KFitServer.DBContext.Models;

namespace KFitServer
{
    public record class ResponseProducts(List<Product> Items);

    public record class YPlaylist(List<Item> Items);

    public record class Item(string Id, Snippet Snippet);

    public record class Snippet(string Title, string Description, Thumbnails Thumbnails);

    public record class Thumbnails(Standard Standard);

    public record class Standard(string Url);
}
