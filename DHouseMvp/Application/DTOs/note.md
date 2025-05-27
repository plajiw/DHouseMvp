Camada Application: o Coração da Orquestração da Sua API

A camada Application é onde você transforma requisições HTTP em ações de negócio concretas, sem “vazar” detalhes do seu modelo de domínio ou dos mecanismos de acesso a dados. Veja como ela se estrutura e quais são seus papéis essenciais:

1. Definição de Casos de Uso (Use Cases)
Interfaces de Serviço (IImovelService, IServicoService) são contratos claros dos comportamentos que sua API oferece:

GetAllAsync(), GetByIdAsync(int id), CreateAsync(dto), UpdateAsync(dto) etc.

Independem da infraestrutura — você programa sua API contra abstrações, não contra classes concretas do Entity Framework ou de outro ORM.

Facilitam o versionamento: quando surgir um novo requisito, basta estender ou adicionar uma nova interface, sem mexer em controllers ou entidades.

2. Orquestração da Lógica de Negócio
Os Services (ImovelService, ServicoService) implementam essas interfaces, injetando:

ApplicationDbContext (camada Infrastructure) para operações de CRUD.

IMapper (AutoMapper) para converter entre DTOs e entidades de domínio.

Fluxo típico dentro de um Service:

Recebe um DTO validado pela camada de API.

Mapeia DTO → Entidade (_mapper.Map<Imovel>(dto)).

Aplica regras (ex.: truncar strings, calcular valores derivados, verificar integridade).

Persiste/recupera com _ctx.SaveChangesAsync() ou _ctx.Imoveis.ToListAsync().

Mapeia de volta Entidade → ResponseDTO para retornar.

3. Transformação e Contrato de Dados
Request DTOs (ImovelCreateDto, ServicoCreateDto): definem apenas o que o cliente pode enviar.

Response DTOs (ImovelResponseDto, ServicoResponseDto): expõem somente o que você deseja revelar (Id, datas, flags, sem vídeos internos ou campos sensíveis).

Evita Over-posting: o cliente não consegue injetar campos adicionais, pois somente os mapeados no CreateDto são considerados.

Evita Under-posting: campos obrigatórios (com [Required]) garantem que o serviço nunca receba dados incompletos.

4. Validação Declarativa
As DataAnnotations nos DTOs ([Required], [MaxLength], [Range], [EmailAddress]) atuam antes do Service:

Se o ModelState falhar, a API devolve 400 Bad Request com as mensagens customizadas (ErrorMessage).

Garante que a camada Application receba sempre um objeto válido, reduzindo checagens manuais dentro dos Services.

5. Desacoplamento e Testabilidade
Controllers dependem de IImovelService/IServicoService, nunca de ApplicationDbContext diretamente.

Nos testes de unidade, você faz mock dessas interfaces e foca em testar apenas a lógica de orquestração (sem envolver banco de dados).

Módulos swapáveis: se quiser trocar EF Core por outro mecanismo (Dapper, repositório em memória), implementa outra classe IImovelService e registra no DI — sem tocar controllers.

