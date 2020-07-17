﻿using Prism.Events;
using ProdData.Extensions;
using ProdData.Models;

namespace ProdData.Events
{
    public class ProgramNamesResponse : PubSubEvent<IndexedObservableCollection<ProgramID>>
    {
    }
}