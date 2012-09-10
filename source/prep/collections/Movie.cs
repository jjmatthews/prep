using System;

namespace prep.collections
{
  public class Movie:IEquatable<Movie>
  {
      string sortingHash;
      public string title { get; set; }
    public ProductionStudio production_studio { get; set; }
    public Genre genre { get; set; }
    public int rating { get; set; }
    public DateTime date_published { get; set; }
      public bool Equals(Movie other)
      {
          return title == other.title;
      }

     
      public override string ToString()
      {
          return string.Format("title: {0}, production_studio: {1}, genre: {2}, rating: {3}", title, production_studio, genre, rating);
      }
  }

   
}