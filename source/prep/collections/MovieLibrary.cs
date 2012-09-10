using System;
using System.Collections;
using System.Collections.Generic;
using prep.utility;

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
            return movies.one_at_a_time();
        }


        public void add(Movie movie)
        {
            if (this.movies.Contains(movie)) return;


            this.movies.Add(movie);
        }

        public IEnumerable<Movie> sort_all_movies_by_title_descending()
        {
            var list = new List<Movie>(movies);

            list.Sort((movie, movie1) => String.CompareOrdinal(movie1.title, movie.title));

            return list;
        }

        public IEnumerable<Movie> all_movies_published_by_pixar()
        {
            //      foreach (var movie in movies)
            //      {
            //        if (movie.production_studio == ProductionStudio.Pixar)
            //          yield return movie;
            //      }
            return movies_published_by(new List<ProductionStudio> { ProductionStudio.Pixar });
        }

        IEnumerable<Movie> movies_published_by(IList<ProductionStudio> productionStudiosToFilter)
        {
            var filteredList = new List<Movie>();
            foreach (var movie in movies)
            {
                if (productionStudiosToFilter.Contains(movie.production_studio))
                    filteredList.Add(movie);
            }

            return filteredList;
        }

        public IEnumerable<Movie> all_movies_published_by_pixar_or_disney()
        {
            return movies_published_by(new List<ProductionStudio> { ProductionStudio.Pixar, ProductionStudio.Disney });

        }

        public IEnumerable<Movie> sort_all_movies_by_title_ascending()
        {
            var list = new List<Movie>(movies);

            list.Sort((movie, movie1) => String.CompareOrdinal(movie.title, movie1.title));

            return list;
        }

        public IEnumerable<Movie> sort_all_movies_by_movie_studio_and_year_published()
        {
            var list = new Dictionary<ProductionStudio, List<Movie>>();

            //MGM
            //Pixar
            //Dreamworks
            //Universal
            //Disney
            list.Add(ProductionStudio.MGM, new List<Movie>());
            list.Add(ProductionStudio.Pixar, new List<Movie>());
            list.Add(ProductionStudio.Dreamworks, new List<Movie>());
            list.Add(ProductionStudio.Universal, new List<Movie>());
            list.Add(ProductionStudio.Disney, new List<Movie>());

            //list.Add(ProductionStudio.Paramount, new List<Movie>());

            foreach (var movie in movies)
            {
                if (movie.production_studio == ProductionStudio.Paramount)
                    continue;

                var aktuelleListe = list[movie.production_studio];
                aktuelleListe.Add(movie);
            }

            foreach (var keyValuePair in list)
            {
                keyValuePair.Value.Sort(delegate(Movie movie, Movie movie1)
                {
                    return DateTime.Compare(movie.date_published, movie1.date_published);
                });

                foreach (var movie in keyValuePair.Value)
                {
                    yield return movie;
                }
            }
        }

        public IEnumerable<Movie> all_movies_not_published_by_pixar()
        {
            IList<ProductionStudio> studios = new List<ProductionStudio> { ProductionStudio.Pixar };
            foreach (var movie in movies)
            {
                if (!studios.Contains(movie.production_studio))
                    yield return movie;
            }
        }

        public IEnumerable<Movie> all_movies_published_after(int year)
        {
            foreach (var movie in movies)
            {
                if (movie.date_published.Year > year)
                    yield return movie;
            }
        }

        public IEnumerable<Movie> all_movies_published_between_years(int startingYear, int endingYear)
        {
            foreach (var movie in movies)
            {
                if (movie.date_published.Year >= startingYear && movie.date_published.Year <= endingYear)
                    yield return movie;
            }
        }

        public IEnumerable<Movie> all_kid_movies()
        {
            foreach (var movie in movies)
            {
                if (movie.genre == Genre.kids)
                    yield return movie;
            }
        }

        public IEnumerable<Movie> all_action_movies()
        {
            foreach (var movie in movies)
            {
                if (movie.genre == Genre.action)
                    yield return movie;
            }
        }

        public IEnumerable<Movie> sort_all_movies_by_date_published_descending()
        {
            var list = new List<Movie>(movies);

            list.Sort((movie, movie1) => DateTime.Compare(movie1.date_published, movie.date_published));

            return list;
        }

        public IEnumerable<Movie> sort_all_movies_by_date_published_ascending()
        {
            var list = new List<Movie>(movies);

            list.Sort((movie, movie1) => DateTime.Compare(movie.date_published, movie1.date_published));

            return list;
        }
    }

    public class LazyEnumerable<T> : IEnumerable<T>
    {
        public LazyEnumerable(IEnumerable<T> movies)
        {
            throw new NotImplementedException();
        }

        public LazyEnumerable()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}