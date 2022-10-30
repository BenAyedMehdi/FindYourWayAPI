﻿using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Services
{
    public class MilestoneService
    {
        private readonly FindYourWayDbContext _context;
        private readonly CompanyService companyService;

        public MilestoneService(FindYourWayDbContext context, CompanyService companyService)
        {
            _context = context;
            this.companyService = companyService;
        }
        public async Task<IEnumerable<Milestone>> GetCompanyMilestones(int id)
        {
            if (!companyService.CompanyExists(id)) return null;
            return await _context.Milestones
                .Where(m => m.CompanyId == id)
                .ToListAsync();
        }
        public async Task<Milestone> GetMilesone(int id)
        {
            var milestone = await _context.Milestones.FindAsync(id);

            return milestone;
        }
        public async Task<Milestone> AddMilestone(AddMilestoneRequest request)
        {
            var company = await companyService.GetCompany(request.CompanyId);
            if (company == null) return null;

            if (request.MilestoneName == null) return null;
            var milestone = new Milestone
            {
                MilestoneName = request.MilestoneName,
                CompanyId = request.CompanyId,
                Company = company
            };

            _context.Milestones.Add(milestone);
            await _context.SaveChangesAsync();

            return milestone;
        }
        public bool MilestoneExists(int id)
        {
            return _context.Milestones.Any(e => e.MilestoneId == id);
        }
    }
}
