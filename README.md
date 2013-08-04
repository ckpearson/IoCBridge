# What?
IoC and Dependency Injection are growing trends in software development. There are a myriad of libraries and frameworks out there for adding IoC to your application.

However, many existing solutions leave you in the dark if you're adding IoC support to your own code-library or you simply want a generic pluggable IoC system in place.

# Why?
Imagine you're creating a code library for consumption by other people, you want to develop it with IoC in mind, so you choose an IoC framework for your development. Congratulations, you've added IoC support to your library, but now the users of your library need to use the same framework in their application or create a wraparound.

IoCBridge offers a completely pluggable IoC solution, allowing you to add your own "wrapper" for any IoC framework you want. It also includes some helpers to get you up and running with generic IoC faster.

# So... it isn't an IoC framework?
No. IoCBridge is a generic base for consuming an IoC framework, this is to allow you to add IoC support to your application / library while allowing the consumer of the library / other dev team to specify an IoC framework for their use.

# Is this compatible with my favourite framework?
IoCBridge isn't directly compatible with any framework, but so long as the framework exposes common IoC functionality a wrapper can be written to add support.