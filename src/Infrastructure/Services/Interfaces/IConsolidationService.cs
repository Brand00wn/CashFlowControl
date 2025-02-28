using Domain.DTOs.Consolidation;
using Domain.DTOs.Launch;
using Domain.Models;

namespace Services.Interfaces;

public interface IConsolidationService
{
    Task<List<LaunchDTO>> CreateConsolidation(List<LaunchDTO> launches);
    Task<List<ConsolidationDTO>> GetAll();
}