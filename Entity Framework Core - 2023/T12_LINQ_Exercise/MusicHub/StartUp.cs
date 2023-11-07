namespace MusicHub
{
    using System;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context = new MusicHubDbContext();

            //DbInitializer.ResetDatabase(context);

            //Console.WriteLine(ExportAlbumsInfo(context, 9));
            Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumInfo = context.Producers
                .Include(x => x.Albums)
                    .ThenInclude(a => a.Songs)
                    .ThenInclude(s => s.Writer)
                .First(x => x.Id == producerId)
                .Albums.Select(x => new
                {
                    AlbumName = x.Name,
                    ReleaseDate = x.ReleaseDate,
                    ProducerName = x.Producer.Name,
                    AlbumSongs = x.Songs.Select(x => new
                    {
                        SongName = x.Name,
                        SongPrice = x.Price,
                        SongWriterName = x.Writer.Name
                    }).OrderByDescending(x => x.SongName)
                        .ThenBy(x => x.SongWriterName),
                    TotalAlbumPrice = x.Price
                }).OrderByDescending(x => x.TotalAlbumPrice).AsEnumerable();

            StringBuilder sb = new();

            foreach (var album in albumInfo)
            {
                sb
                    .AppendLine($"-AlbumName: {album.AlbumName}")
                    .AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}")
                    .AppendLine($"-ProducerName: {album.ProducerName}")
                    .AppendLine("-Songs:");

                if (album.AlbumSongs.Any())
                {
                    int num = 1;

                    foreach (var song in album.AlbumSongs)
                    {
                        sb
                            .AppendLine($"---#{num}")
                            .AppendLine($"---SongName: {song.SongName}")
                            .AppendLine($"---Price: {song.SongPrice:F2}")
                            .AppendLine($"---Writer: {song.SongWriterName}");

                        num++;
                    }
                }

                sb.AppendLine($"-AlbumPrice: {album.TotalAlbumPrice:F2}");
            }

            return sb.ToString().Trim();
        }

        //public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        //{
        //    var songs = context.Songs
        //        .Include(s => s.SongPerformers)
        //            .ThenInclude(sp => sp.Performer)
        //        .Include(s => s.Writer)
        //        .Include(s => s.Album)
        //            .ThenInclude(a => a.Producer)
        //        .ToList()
        //        .Where(s => s.Duration.TotalSeconds > duration)
        //        .Select(s => new
        //        {
        //            s.Name,
        //            Performers = s.SongPerformers
        //                .Select(sp => sp.Performer.FirstName + " " + sp.Performer.LastName)
        //                .ToList(),
        //            WriterName = s.Writer.Name,
        //            AlbumProducer = s.Album.Producer.Name,
        //            Duration = s.Duration.ToString("c")
        //        })
        //        .OrderBy(s => s.Name)
        //            .ThenBy(s => s.WriterName);
        //    //.ToList();

        //    StringBuilder sb = new();

        //    int counter = 1;
        //    foreach (var song in songs)
        //    {
        //        sb
        //            .AppendLine($"-Song #{counter}")
        //            .AppendLine($"---SongName: {song.Name}")
        //            .AppendLine($"---Writer: {song.WriterName}");


        //        if (song.Performers.Any())
        //        {
        //            if (song.Performers.Count == 1)
        //            {
        //                sb.AppendLine($"---Performer: {string.Join("", song.Performers)}");
        //            }
        //            else
        //            {
        //                sb.AppendLine($"---Performers: {string.Join(",", song.Performers)}");
        //            }
        //        }

        //        sb
        //            .AppendLine($"---AlbumProducer: {song.AlbumProducer}")
        //            .AppendLine($"---Duration: {song.Duration}");

        //        counter++;
        //    }



        //    return sb.ToString().Trim();
        //}

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .Include(s => s.SongPerformers)
                .ToList()
                .Where(s => s.Duration.TotalSeconds > duration)
                .OrderBy(s => s.Name)
                .ThenBy(s => s.Writer.Name)
                .ToList();

            var result = new StringBuilder();
            var songNumber = 1;

            foreach (var s in songs)
            {
                result.AppendLine($"-Song #{songNumber++}");
                result.AppendLine($"---SongName: {s.Name}");
                result.AppendLine($"---Writer: {s.Writer.Name}");

                if (s.SongPerformers.Count > 0)
                {
                    var performers = s.SongPerformers
                        .OrderBy(sp => sp.Performer.FirstName)
                        .ThenBy(sp => sp.Performer.LastName);

                    foreach (var p in performers)
                    {
                        var fullName = p.Performer.FirstName + " " + p.Performer.LastName;

                        result.AppendLine($"---Performer: {fullName}");
                    }
                }

                result.AppendLine($"---AlbumProducer: {s.Album.Producer.Name}");
                result.AppendLine($"---Duration: {s.Duration.ToString("c")}");
            }

            return result.ToString().Trim();
        }
    }
}
