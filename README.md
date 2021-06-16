# Liner.WPF - interview project


## For that project some fetures should be implemented but i didn't have so much time:

###### Architecture

#1 - Liner.API for simplicity are class libraries, in production env should be external API and publicated contracts

#2 - Proper UnitTest should be written for whole Lines.Core domain business logic models

#3 - UnitTests with testing framework like NUnit, libraries - AuotFixture, FluentAssertions, Moq

#4 - ILogger implementation for storing logs in some persist storage

#5 - Some kind of AoP approach for logging business Exceptions from API

#6 - Maybe AutoMapper library for mapping Contracts <-> CommandRequests <-> BuildCoreDomainModels (I preffer explicit creation of DomainModels)

###### Algorithm

4.1 - BFS would be significantly faster with dedicated nodes not Node<TValue>, explicit memory allocations(unsafe)

4.2 - In this case data volume is very small - only 206k nodes with max 4 childrens

4.2 - For bigger volume more complex algorithm should be used including paraller execution
