using System.Net.Http.Json;
using Istebrek_CV_Webb_FE.Models;

namespace Istebrek_CV_Webb_FE.Data
{
    public class SkillsService
    {
        private readonly HttpClient _http;

        public SkillsService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Technology>> GetTechnologiesAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<Technology>>("/technologies");
            } 
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task AddTechnologyAsync(Technology tech)
        {
            try
            {
                await _http.PostAsJsonAsync("/technology", tech);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task UpdateTechnologyAsync(string id, Technology tech)
        {
            try
            {
                await _http.PutAsJsonAsync($"/technology/{id}", tech);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task DeleteTechnologyAsync(string id)
        {
            try
            {
                await _http.DeleteAsync($"/technology/{id}");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
       
    }

}

