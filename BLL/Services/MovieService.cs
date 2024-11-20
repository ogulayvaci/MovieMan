using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class MovieService : Service, IService<movie, MovieModel>
{
    public MovieService(Db db) : base(db)
    {
    }

    public IQueryable<MovieModel> Query()
    {
        return _db.movie
            .Include(m => m.director)
            .Include(m => m.moviegenre)
            .OrderByDescending(m => m.name)
            .ThenBy(m => m.releasedate)
            .Select(m => new MovieModel { Record = m });
    }

    public Service Create(movie entity)
    {
        if (_db.movie.Any(m => m.name.ToLower() == entity.name.ToLower()))
            return Error("Movie already exists");
        _db.movie.Add(entity);
        _db.SaveChanges();
        return Success("Movie created");
    }

    public Service Update(movie entity)
    {
        if (_db.movie.Any(m => m.id != entity.id && m.name.ToLower() == entity.name.ToLower()))
            return Error("Movie with the same name already exists");
        var rec = _db.movie.FirstOrDefault(m => m.id == entity.id);
        rec.name = entity.name;
        rec.releasedate = entity.releasedate;
        rec.director = entity.director;
        rec.moviegenre = entity.moviegenre;
        rec.totalrevenue = entity.totalrevenue;
        _db.movie.Update(rec);
        _db.SaveChanges();
        return Success("Movie updated");
    }

    public Service Delete(int id)
    {
        var movie = _db.movie.Include(m=>m.moviegenre).FirstOrDefault(m => m.id == id);
        if(movie is null)
            return Error("Movie not found");
        if(movie.moviegenre.Any())
            return Error("Movie has movie genre record(s).");
        _db.movie.Remove(movie);
        _db.SaveChanges();
        return Success("Movie deleted");
    }
}