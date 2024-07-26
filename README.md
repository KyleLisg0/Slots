# Slots
Repository for the basic implementation of a slot machine

## Pull the level and have a go!
A really basic implementation of a slot machine to exercise some problem solving.

The main **SlotMachine** class implemented as an IHostedService so it can run on a background thread and uses a nice cancellation source of Ctrl + C if you want to end it.
We might be better off removing it later.

In the real world we'd implement things like the wallet as async calls out to external resources (databases etc.), but that's a bit much for this project.

**ReelGenerator** handles line and symbol random generation rather than implementing as a separate service. It keeps the dependencies cleaner and the debugging easier and we could easily extract if required.

## Tests are in BDD style with Reqnroll!
I like BDD - Ask me about it!

It's not for everyone and it's not inseperable from specflow/reqnroll as people sometimes assume, so I'm well used to regular style, repetitive unit tests. 

The exception is the **ReelGenerator**, which is just a POUT (Plain Old Unit Test) because the end-to-end stuff has to mock out the randomness and it saves writing another feature file.