using System;
using System.Collections.Generic;
using XamarinSocial.Models.Chat;
using LocalStrings = XamarinSocial.Resources.Strings.Strings;

namespace XamarinSocial.Extensions
{
    public static class ChatConversationDateTimeExtension
    {
        private static bool GetTopDateVisibility(DateTime firstDate, DateTime secondDate)
        {
            //todo:change != to >
            return secondDate.Date != firstDate.Date ? true : false;
        }

        public static void GetTopDateText(this ChatConversationItemModel item)
        {
            if (item.CreationDate.DayOfYear == DateTime.Now.DayOfYear
                && item.CreationDate.Year == DateTime.Now.Year)
            {
                item.TopDateString = $"{LocalStrings.Today}, {item.CreationDate.Day} {item.CreationDate.ToString("MMMM")}";
                return;
            }
            if (item.CreationDate.DayOfYear == DateTime.Now.DayOfYear - 1
                && item.CreationDate.Year == DateTime.Now.Year)
            {
                item.TopDateString = $"{LocalStrings.Yesterday}, {item.CreationDate.Day} {item.CreationDate.ToString("MMMM")}";
                return;
            }

            item.TopDateString = item.CreationDate.ToString("dd MMMM, yyyy");
        }

        public static ChatConversationItemModel SetTopDateVisibility(this List<ChatConversationItemModel> items, ChatConversationItemModel previousItem)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (previousItem != null && i == 0)
                {
                    previousItem.HasTopDate = GetTopDateVisibility(items[i].CreationDate, previousItem.CreationDate);
                    previousItem.GetTopDateText();
                    continue;
                }
                if (i > 0)
                {
                    items[i-1].HasTopDate = GetTopDateVisibility(items[i].CreationDate, items[i-1].CreationDate);
                }
                if (i > 0 && items[i - 1].HasTopDate)
                {
                    items[i - 1].GetTopDateText();
                }
            }
            return previousItem;
        }

        public static ChatConversationItemModel SetNewItemTopTextVisibility(this ChatConversationItemModel newItem, ChatConversationItemModel lastItem)
        {
            if (lastItem == null)
            {
                return lastItem;
            }
            newItem.HasTopDate = GetTopDateVisibility(lastItem.CreationDate, newItem.CreationDate);
            newItem.GetTopDateText();
            if (newItem.HasTopDate)
            {
                lastItem.HasTopDate = false;
                lastItem.TopDateString = string.Empty;
            }
            return lastItem;
        }
    }
}
