using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TGbot
{
    class Bot
    {
        static TelegramBotClient token = new TelegramBotClient("ТОКЕН");
        static SaveMessages newMess = new SaveMessages();
        static game Game = new game();

        public Bot() 
        {
            token.StartReceiving(Update, Error);
        }
        private async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message.Text != null)
            {
                string id = Convert.ToString(message.Chat.Id);

                if (message.Text == "/start")
                {
                    string word = game.getWord();
                    string shifredW = game.shifrW(word);

                    if (newMess.findUser(id) == false)
                    {
                        newMess.addUser(id, word, shifredW);
                    }
                    else
                    {
                        newMess.reNewUser(id, word, shifredW);
                    }
                    await client.SendTextMessageAsync(message.Chat.Id, $"Веше слово:\n{shifredW}\nBведіть літеру!");

                }
                else if (message.Text.Length == 1)
                {
                    if (newMess.findUser(id))
                    {
                        char letter = message.Text.ToLower()[0];
                        string word = newMess.getItem(id, "word");
                        string shifredW = newMess.getItem(id, "shifr");

                        string game = Game.gaming(letter, word, shifredW);

                        int thisTry = Convert.ToInt32(newMess.getItem(id, "try")) + 1;

                        if (game == shifredW && game != word)
                        {

                            if (thisTry == 6)
                            {
                                await client.SendTextMessageAsync(message.Chat.Id, $"Веше слово:\n{word}\nВи програли :(");
                                await client.SendTextMessageAsync(message.Chat.Id, Game.textPicture(6));

                                string loses = Convert.ToString(Convert.ToInt32(newMess.getItem(id, "loses")) + 1);

                                newMess.reNewItem(id, "loses", loses);
                                newMess.reNewItem(id, "shifr", word);
                            }
                            else if (thisTry < 6)
                            {
                                newMess.reNewItem(id, "try", Convert.ToString(thisTry));
                                await client.SendTextMessageAsync(message.Chat.Id, Game.textPicture(thisTry));
                            }

                        }
                        else if(game == word && shifredW != word)
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, $"Веше слово:\n{word}\nВи перемогли :) \nВведіть '/start'");

                            string wins = Convert.ToString(Convert.ToInt32(newMess.getItem(id, "wins")) + 1);

                            newMess.reNewItem(id, "wins", wins);
                            newMess.reNewItem(id, "shifr", game);
                        }
                        else
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, game);
                            newMess.reNewItem(id, "shifr", game);
                        }

                    }
                }
                else if (message.Text == "/statistics")
                {
                    await client.SendTextMessageAsync(message.Chat.Id, newMess.getStat(id));
                }
                else if (message.Text == "/rules")
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "Правила:\n" +
                        "1. Вводити лише лише по одній літері\n" +
                        "2. Апостроф не враховується\n" +
                        "3. Всі слова Українські");
                }
                else
                {
                    newMess.save(message.Text, message.Chat.Username);
                    await client.SendTextMessageAsync(message.Chat.Id, "Не коректний ввід, спробуйте '/start', '/statistics', або '/rules'");
                }
            }
        }

        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
