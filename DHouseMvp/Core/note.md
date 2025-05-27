**Objetivo**: definir o modelo de domínio puro, sem depender de frameworks ou detalhes de infraestrutura
Vamos definir os modelos centrais: Imovel e ServicoOferecido

São independentes de como os dados serão armazenados (banco de dados), como serão apresentados ao usuário (API, interface gráfica) ou de quaisquer bibliotecas externas específicas (frameworks de UI, ORMs específicos, etc.).
Essa independência torna a camada Core altamente reutilizável, mais fácil de testar isoladamente e menos suscetível a mudanças em outras partes do sistema.

Entidade: Imovel
A classe Imovel representa um imóvel no seu sistema. Ela define quais informações um imóvel possui.

Entidade: ServicoOferecido
A classe ServicoOferecido representa os diferentes serviços que a imobiliária pode oferecer, talvez relacionados aos imóveis ou aos clientes.

