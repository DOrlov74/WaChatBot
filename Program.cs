// See https://aka.ms/new-console-template for more information
using WaChatBot.Services;

Console.WriteLine("Введите API token");
string? token = Console.ReadLine();
while (token == null)
{
  Console.WriteLine($"Ошибка ввода. Попробуйте ещё раз");
  token = Console.ReadLine();
};
Console.WriteLine($"Получен API токен");
BotService botService = new BotService(token);
if (await botService.Start())
{
  Console.WriteLine($"Получен Instance Id");
  Console.WriteLine("Введите номер клиента в формате '1234567890'");
  string? clientNum = Console.ReadLine();
  while (Utils.IsPhoneNbr(clientNum))
  {
    Console.WriteLine($"Ошибка ввода. Попробуйте ещё раз");
    clientNum = Console.ReadLine();
  };
  Console.WriteLine($"Получен номер клиента");
  clientNum = Utils.CleanPhoneNumber(clientNum);
  string greeting = "Здравствуйте! Я консультант помогу подобрать запасную часть и отвечу на Ваши вопросы. Что Вас интересует?";
  if (await botService.SendMessage(clientNum, greeting)) 
  {
    Console.WriteLine($"Отправлено приветствие");
    bool exit = false;
    do
    {
      greeting = "Возможные варианты: Подбор запчасти / Доставка запчасти / Оплата запчасти / Установка запчасти / Возврат запчасти";
      await botService.SendMessage(clientNum, greeting);
      List<string> items = new()
    {
      "подбор", "доставка", "оплата", "установка", "возврат"
    };
      string selected = await botService.GetSelection(clientNum, items);
      if (!string.IsNullOrEmpty(selected))
      {
        Console.WriteLine($"Выбрано: {selected}");
        string message = "";
        switch (selected)
        {
          case "подбор":
            message = "укажите БРЕНД";
            await botService.SendMessage(clientNum, message);
            message = "Выберите и закажите свою запчасть по каталогу производителя  <ссылка: https://spareparts.sale/katalogi/>";
            break;
          case "доставка":
            message = "Вы уже оформили заказ? (да/нет)";
            List<string> selection = new() { "да", "нет" };
            await botService.SendMessage(clientNum, message);
            string answer = await botService.GetSelection(clientNum, selection);
            switch (answer)
            {
              case "да":
                message = "На данном номере производится консультация по подбору запчастей. Инофмацию по текущему статусу заказа Вы можете проверить в личном кабинете или запросить";
                break;
              case "нет":
                message = "Воспользуйтесь калькуляторами доставки <ссылка: https://spareparts.sale/delivery>";
                break;
            }
            break;
          case "оплата":
            message = "Информация по способам оплаты товара <ссылка: https://spareparts.sale/oplata>";
            break;
          case "установка":
            message = "Установку запчасти Вы можете произвести в  техническом центре ТАЛИОН СЕРВИС  <ссылка: https://talion.ru/>";
            break;
          case "возврат":
            message = "укажите номер ЗАКАЗА";
            break;
        }
        if (!string.IsNullOrEmpty(message))
        {
          if (await botService.SendMessage(clientNum, message))
          {
            if (selected == "подбор")
            {
              message = "Переключаем на консультанта...";
              await botService.SendMessage(clientNum, message);
              exit = true;
            }
            else { 
              message = "Остались вопросы? (да/нет)";
              await botService.SendMessage(clientNum, message);
              List<string> selection = new() { "да", "нет" };
              string answer = await botService.GetSelection(clientNum, selection);
              switch (answer)
              {
                case "да":
                  message = "";
                  break;
                case "нет":
                  message = "Рекомендуем ознакомиться с видеоинструкцией по работе магазина  <ссылка: https://www.youtube.com/watch?v=zhpN-p5eKTU>";
                  await botService.SendMessage(clientNum, message);
                  break;
              }
            }
          }
          else
          {
            Console.WriteLine($"Сообщение {message} не отправлено");
          }
        }
      }
      else
      {
        Console.WriteLine("Ответ не распознан");
      }
    }while (!exit);
  } else
  {
    Console.WriteLine("Приветствие не отправлено");
  }
} else
{
  Console.WriteLine("Ошибка получения Instance Id");
}


