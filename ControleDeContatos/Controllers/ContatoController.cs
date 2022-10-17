using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Controllers
{

    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio) {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {

            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {

            ContatoModel contato =_contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }


        public IActionResult Apagar(int id) {

            try {

               bool apagado = _contatoRepositorio.Apagar(id);

                if (apagado) {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                } 
                else {
                    TempData["MensagemErro"] = "Não foi possivel apagar seu contato apagado com sucesso";
                }

                return RedirectToAction("Index");

            }
            catch (Exception erro) {

                TempData["MensagemErro"] = $"Erro! Não foi possivel cadastrar seu contato. Detalhe do erro: {erro.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato) {

            try {
                if (ModelState.IsValid) {

                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(contato);

            } catch (System.Exception erro) {
                TempData["MensagemErro"] = $"Erro! Não foi possivel cadastrar seu contato. Detalhe do erro: {erro.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato) {


            try {

                if (ModelState.IsValid) {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);

            }
            catch (Exception erro) {
                TempData["MensagemSucesso"] = $"Erro! Não foi possivel atualizar seu contato. Detalhe do erro: {erro.Message} ";
                return RedirectToAction("Index");

            }

        }
    }
}
