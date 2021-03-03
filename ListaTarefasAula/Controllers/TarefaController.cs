using DataAcess;
using DataAcess.Migrations;
using DataAcess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaTarefasAula.Controllers
{
    public class TarefaController : Controller
    {
        public ITarefaRepository _Tarefa { get; set; }
        public TarefaController(ITarefaRepository tarefa)
        {
            _Tarefa = tarefa;
        }
        public IActionResult Index()
        {
            var objs = new TarefaVM
            {
                Tarefa = new Tarefa(),
                Tarefas = _Tarefa.GetAll().OrderBy(a => a.Status)
            };

            return View(objs);
        }
        [HttpPost]
        public IActionResult Upsert(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {

                if (tarefa.Id == 0)
                {
                    var obj = tarefa;
                    obj.Status = Status.INICIAR;

                    _Tarefa.Add(tarefa);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _Tarefa.Update(tarefa);
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View();
            }

        }

        public IActionResult AtualizarStatusTarefa(int id)
        {


            var obj = _Tarefa.Get(id);
            _Tarefa.AtualizarStatus(obj);
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Delete(int? id)
        {
            if (id != 0)
            {

                _Tarefa.Remove(id);

                RedirectToAction(nameof(Index));
            }


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            var obj = _Tarefa.Get(id);


            return View(obj);
        }

        public IActionResult Visualizar(int? id)
        {
            var obj = _Tarefa.Get(id);


            return View(obj);
        }

    }
}
