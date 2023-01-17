using CinemaApp.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Seats : ContentPage
    {
        public Seats(Showtime showtime)
        {
            InitializeComponent();
            SelectedTicket = showtime;
            InIt();
            this.BindingContext = this;
        }

        public Showtime SelectedTicket { get; set; }

        Dictionary<int, int> data = new Dictionary<int, int>();
        SKPaint availablePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#FF0000")
        };

        SKPaint rerservedPaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#00FF00")
        };

        SKPaint yourSeatPaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#FFFF00")
        };

        SKPaint textPaint = new SKPaint()
        {
            TextSize = 40,
            Color = SKColor.Parse("#FFFFFF")
        };

        private void InIt()
        {
            var rand = new Random();
            for (int i = 0; i < 120; i++)
            {
                data.Add(i,rand.Next(0,2));
            }
        }
        private void canvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            var x = 60;
            var y = 60;
            var row = -1;
            var column = 14;
            var itemHeight = 40;
            var itemWidth = 40;
            var margin = 20;
            var cornerRadius = 4;
            var items = 0;

            for(int i =0; i < data.Count; i++)
            {
                if (items == 0)
                {
                    row += 1;
                    items = GetColumn(row);
                    var count = (column - items) / 2;
                    var offset = (count*itemWidth) + (count*margin);
                    x = 60 + offset;
                    y = (itemWidth + ((itemWidth + margin) * row));
                }
                else
                {
                    x += itemHeight + margin;
                }
                var setColorIndex = data[i];

                //if (SelectedTicket.Seats.Any(z => z == i))
                //    setColorIndex = 2;

                canvas.DrawRoundRect(new SKRoundRect(new SKRect(x, y, x + itemHeight, y+itemWidth), cornerRadius), GetColor(setColorIndex));
            items -= 1;

            if (items == 0)
            {
                canvas.DrawText($"{row + 1}", 0, y + margin + (itemHeight / 2), textPaint);
            }
            }
        }

        SKPaint GetColor(int seatColor)
    {
        switch (seatColor)
        {
            case 0:
            default:
                return availablePaint;
            case 1:
                return rerservedPaint;
            case 2:
                return yourSeatPaint;
        }
    }
    private int GetColumn(int row)
        {
            switch (row)
            {
                case 0:
                    return 8;
                case 1:
                case 9:
                    return 10;
                case 2:
                case 3:
                case 8:
                    return 12;
                default:
                    return 14;
            }
        }

    }
}