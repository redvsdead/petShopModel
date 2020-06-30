using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace petShopModel
{
    abstract class Department
    {
        protected Department Next;          //паттерн chain of command
        protected Contractor contractor;    //поставщик отдела
        protected DeliveryMan deliverer;    //доставщик отдела

        public event Action<Purchase, Contractor> RequestFinished;

        protected Department()
        {
            contractor = CreateEmployee();
            contractor.RequestFinished += (request, employee) => RequestFinished?.Invoke(request, employee);
        }

        /// <summary>
        /// Метод для связывания двух отделов
        /// </summary>
        /// <param name="department">Новый отдел</param>
        /// <returns>Последний добавленный отдел</returns>
        public Department SetNext(Department department)
        {
            Next = department;
            return department;
        }

        /// <summary>
        /// Добавить действие на событие для этого и последующих отделов
        /// </summary>
        /// <param name="action"></param>
        public void Subscribe(Action<Purchase, Contractor> action)
        {
            RequestFinished += action;
            Next?.Subscribe(action);
        }

        /// <summary>
        /// Может ли отделение обработать поступающую заявку
        /// </summary>
        /// <param name="request">Заявка</param>
        /// <returns>Может ли отделение обработать поступающую заявку</returns>
        protected abstract bool CanHandle(Purchase request);

        /// <summary>
        /// Фабричный метод для создания нового сотрудника
        /// </summary>
        /// <returns>Новый объект сотрудника</returns>
        protected abstract Contractor CreateEmployee();

        /// <summary>
        /// Обработка заявки
        /// </summary>
        /// <param name="request">Поступающая заявка</param>
        /// <param name="context">Контекст синхронизации потоков</param>
        /// <returns>Была ли обработана заявка</returns>
        public bool HandleRequest(Purchase request, SynchronizationContext context)
        {
            //Если этот отдел не может обработать заявку, отправить в следующий
            if (!CanHandle(request))
                return Next != null && Next.HandleRequest(request, context);

            //Если сотрудник всё ещё работает над предыдущей заявкой,
            //обработка заявки пока невозможна
            if (contractor.IsBusy)
                return false;

            //Иначе сотрудник начинает работу над заявкой
            contractor.Process(request, context);
            return true;
        }
    }
}

    class rodentDepartment
    {

    }
    //ну и тут наследники-отделы
