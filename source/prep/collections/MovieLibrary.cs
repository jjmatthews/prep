using System;
using System.Collections.Generic;

namespace prep.collections
{
  public class MovieLibrary
  {
    IList<Movie> movies;

    public MovieLibrary(IList<Movie> list_of_movies)
    {
      this.movies = list_of_movies;
    }

    public IEnumerable<Movie> all_movies()
    {
      return this.movies;
    }

    public void add(Movie movie)
    {
        if(!movies.Contains(movie))
            movies.Add(movie);
    }

    public IEnumerable<Movie> sort_all_movies_by_title_descending()
    {
        var sortedMovies = new List<Movie>(movies);
        sortedMovies.Sort(new MovieTitlePublished(SortingDirection.Desc));

        return sortedMovies;
    }

    public IEnumerable<Movie> all_movies_published_by_pixar()
    {

        var filteredMovies = new List<Movie>();
        foreach (var movie in movies)
        {
            if (movie.production_studio == ProductionStudio.Pixar)
                filteredMovies.Add(movie);
        }
        return filteredMovies;
    }

    public IEnumerable<Movie> all_movies_published_by_pixar_or_disney()
    {
        var filteredMovies = new List<Movie>();
        foreach (var movie in movies)
        {
            if (movie.production_studio == ProductionStudio.Pixar || movie.production_studio == ProductionStudio.Disney)
                filteredMovies.Add(movie);
        }
        return filteredMovies;
    }

    public IEnumerable<Movie> sort_all_movies_by_title_ascending()
    {
        var sortedMovies = new List<Movie>(movies);
        sortedMovies.Sort(new MovieTitlePublished(SortingDirection.Asc));

        return sortedMovies;
    }

    public IEnumerable<Movie> sort_all_movies_by_movie_studio_and_year_published()
    {
        var list = new List<Movie>(movies);

        list.Sort(new MovieStudioYearPublishedSort());

        return list;
    }

    public IEnumerable<Movie> all_movies_not_published_by_pixar()
    {
        var filteredMovies = new List<Movie>();
        foreach (var movie in movies)
        {
            if (movie.production_studio != ProductionStudio.Pixar)
                filteredMovies.Add(movie);
        }
        return filteredMovies;
    }

    public IEnumerable<Movie> all_movies_published_after(int year)
    {
        var filteredMovies = new List<Movie>();
        foreach (var movie in movies)
        {
            if (movie.date_published.Year >year)
                filteredMovies.Add(movie);
        }
        return filteredMovies;
    }

    public IEnumerable<Movie> all_movies_published_between_years(int startingYear, int endingYear)
    {
        var filteredMovies = new List<Movie>();
        foreach (var movie in movies)
        {
            if (movie.date_published.Year >= startingYear && movie.date_published.Year <=endingYear)
                filteredMovies.Add(movie);
        }
        return filteredMovies;
    }

    public IEnumerable<Movie> all_kid_movies()
    {
        var filteredMovies = new List<Movie>();
        foreach (var movie in movies)
        {
            if (movie.genre == Genre.kids)
                filteredMovies.Add(movie);
        }

        return filteredMovies;
    }

    public IEnumerable<Movie> all_action_movies()
    {
        var actionMovies = new List<Movie>();
        foreach (var movie in movies)
        {
            if(movie.genre == Genre.action)
                actionMovies.Add(movie);
        }

        return actionMovies;
    }

    public IEnumerable<Movie> sort_all_movies_by_date_published_descending()
    {
        var sortedMovies = new List<Movie>(movies);
        sortedMovies.Sort(new MovieDatePublished(SortingDirection.Desc));

        return sortedMovies;
    }

    public IEnumerable<Movie> sort_all_movies_by_date_published_ascending()
    {
        var sortedMovies = new List<Movie>(movies);
        sortedMovies.Sort(new MovieDatePublished(SortingDirection.Asc));

        return sortedMovies;
    }
  }
  public class MovieTitlePublished : IComparer<Movie>
  {
      readonly SortingDirection sortingDirection;

      public MovieTitlePublished(SortingDirection sortingDirection)
      {
          this.sortingDirection = sortingDirection;
      }

      public int Compare(Movie x, Movie y)
      {
          switch (sortingDirection)
          {
              case SortingDirection.Asc:
                  return x.title.CompareTo(y.title);

                  break;
              case SortingDirection.Desc:
                  return -x.title.CompareTo(y.title);

                  break;
              default:
                  throw new ArgumentOutOfRangeException();
          }
      }
  }

    public class MovieDatePublished : IComparer<Movie>
    {
        readonly SortingDirection sortingDirection;

        public MovieDatePublished(SortingDirection sortingDirection)
        {
            this.sortingDirection = sortingDirection;
        }

        public int Compare(Movie x, Movie y)
        {
            switch (sortingDirection)
            {
                case SortingDirection.Asc:
                    return x.date_published.CompareTo(y.date_published);

                    break;
                case SortingDirection.Desc:
                    return -x.date_published.CompareTo(y.date_published);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class MovieStudioYearPublishedSort : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
          return  x.production_studio.GetHashCode().CompareTo(y.production_studio.GetHashCode()) &
            x.date_published.Year.CompareTo(y.date_published.Year);
        }
    }

    public enum SortingDirection
    {
        Asc,
        Desc
    }
}