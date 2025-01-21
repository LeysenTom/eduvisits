using Microsoft.EntityFrameworkCore;
using MVCProject.Data.repository;
using MVCProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AzureDbContext _context;

        public UnitOfWork(AzureDbContext context)
        {
            _context = context;
            AfwezigheidRepository = new Repository<Afwezigheid>(_context);
            FotoalbumRepository = new Repository<FotoAlbum>(_context);
            GebruikerRepository = new Repository<Gebruiker>(_context);
            KlasRepository = new Repository<Klas>(_context);
            NavormingRepository = new Repository<Navorming>(_context);
            StudiebezoekRepository = new Repository<Studiebezoek>(_context);
            VakRepository = new Repository<Vak>(_context);
            BegeleidingRepository = new Repository<Begeleiding>(_context);
            BijlageRepository = new Repository<Bijlage>(_context);
            FotoRepository = new Repository<Foto>(_context);
            GebruikerNavormingRepository = new Repository<GebruikerNavorming>(_context);
            KlasStudiebezoekRepository = new Repository<KlasStudiebezoek>(_context);
        }

        public IRepository<Afwezigheid> AfwezigheidRepository { get; } = default!;
        public IRepository<FotoAlbum> FotoalbumRepository { get; } = default!;
        public IRepository<Gebruiker> GebruikerRepository { get; } = default!;
        public IRepository<Klas> KlasRepository { get; } = default!;
        public IRepository<Navorming> NavormingRepository { get; } = default!;
        public IRepository<Studiebezoek> StudiebezoekRepository { get; } = default!;
        public IRepository<Vak> VakRepository { get; } = default!;
        public IRepository<Begeleiding> BegeleidingRepository { get; } = default!;
        public IRepository<Bijlage> BijlageRepository { get; } = default!;
        public IRepository<Foto> FotoRepository { get; } = default!;
        public IRepository<GebruikerNavorming> GebruikerNavormingRepository { get; } = default!;
        public IRepository<KlasStudiebezoek> KlasStudiebezoekRepository { get; } = default!;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Gebruiker>GetUserByEmail(string email)
        {
            return await _context.Gebruikers.FirstAsync(g => g.Email == email);
        }
    }
}