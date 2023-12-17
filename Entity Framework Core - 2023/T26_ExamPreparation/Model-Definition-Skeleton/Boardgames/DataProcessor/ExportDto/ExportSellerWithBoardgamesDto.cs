using System;
namespace Boardgames.DataProcessor.ExportDto
{
    public class ExportSellerWithBoardgamesDto
    {
        public ExportSellerWithBoardgamesDto()
        {
            List<ExportBoardgameDto> Boardgames = new();
        }
        public string Name { get; set; }
        public string Website { get; set; }
        public ExportBoardgamesForSellerDto[] Boardgames { get; set; }
    }
}

