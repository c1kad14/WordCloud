using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using WordCloud.Abstractions;
using WordCloud.Entities;

using WordCloud.Models;


namespace WordCloud.Controllers
{
    //main controller class
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataRepository<Word> _repository;
        private readonly IHashService _hashService;
        private readonly IWordService _wordService;

        public HomeController(ILogger<HomeController> logger, IDataRepository<Word> repository, IHashService hashService, IWordService wordService)
        {
            _logger = logger;
            _repository = repository;
            _hashService = hashService;
            _wordService = wordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cloud(UrlModel model)
        {
            try
            {
                var wordList = _wordService.Get(model.Url);
                var sortedWordDictionary = _wordService.CalculateAndSortToDictionary(wordList);

                foreach (var w in sortedWordDictionary)
                {
                    var word = new Word { Value = w.Key, Count = w.Value };
                    var existing = await _repository.Get(word.Value);

                    if (existing != null)
                    {
                        existing.Count += word.Count;
                        _repository.Update(existing);
                    }
                    else
                    {
                        word.Id = _hashService.Hash(word.Value);
                        _repository.Add(word);
                    }
                }

                ViewBag.Words = sortedWordDictionary.ToDictionary(w => w.Key, w => w.Value); ;
            }
            catch
            {
                return RedirectToAction("Error");
            }

            return View("Index");
        }

        public async Task<IActionResult> Admin()
        {
            var words = await _repository.Get();
            ViewBag.Words = words?.OrderByDescending(w => w.Count).Select(w => new Word { Id = w.Id, Value = w.Value, Count = w.Count });
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
