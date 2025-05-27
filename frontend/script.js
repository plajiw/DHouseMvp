document.addEventListener('DOMContentLoaded', () => {
    const API_IMOVEIS_URL = 'http://localhost:5000/api/imoveis';
    const API_SERVICOS_URL = 'http://localhost:5000/api/servicos';

    let currentTab = 'imoveis';
    let editingId = null;

    // --- Elementos Comuns ---
    const messagesArea = document.getElementById('messagesArea');
    const imoveisTabButton = document.getElementById('imoveis-tab');
    const servicosTabButton = document.getElementById('servicos-tab');

    // --- Elementos do Formulário e Lista de Imóveis ---
    const imovelForm = document.getElementById('imovelForm');
    const imovelIdField = document.getElementById('imovelId');
    const imovelTituloField = document.getElementById('imovelTitulo');
    const imovelDescricaoField = document.getElementById('imovelDescricao');
    const imovelPrecoField = document.getElementById('imovelPreco');
    const imovelPublicadoField = document.getElementById('imovelPublicado');
    const imovelSubmitButton = document.getElementById('imovelSubmitButton');
    const imovelCancelEditButton = document.getElementById('imovelCancelEditButton');
    const imovelFormCardTitle = document.getElementById('imovelFormCardTitle');
    const loadImoveisButton = document.getElementById('loadImoveisButton');
    const imoveisListEl = document.getElementById('imoveisList');

    // --- Elementos do Formulário e Lista de Serviços ---
    const servicoForm = document.getElementById('servicoForm');
    const servicoIdField = document.getElementById('servicoId');
    const servicoNomeField = document.getElementById('servicoNome');
    const servicoDescricaoField = document.getElementById('servicoDescricao');
    const servicoPrecoField = document.getElementById('servicoPreco');
    const servicoAtivoField = document.getElementById('servicoAtivo');
    const servicoSubmitButton = document.getElementById('servicoSubmitButton');
    const servicoCancelEditButton = document.getElementById('servicoCancelEditButton');
    const servicoFormCardTitle = document.getElementById('servicoFormCardTitle');
    const loadServicosButton = document.getElementById('loadServicosButton');
    const servicosListEl = document.getElementById('servicosList');

    // --- Funções Utilitárias ---
    function showMessage(message, type = 'success') {
        const wrapper = document.createElement('div');
        const alertType = type === 'success' ? 'success' : 'danger';
        wrapper.className = `alert alert-${alertType} alert-dismissible fade show m-0`;
        wrapper.setAttribute('role', 'alert');
        wrapper.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        `;
        messagesArea.innerHTML = '';
        messagesArea.appendChild(wrapper);
        setTimeout(() => {
            if (wrapper.parentElement) {
                const alertInstance = bootstrap.Alert.getOrCreateInstance(wrapper);
                if (alertInstance) alertInstance.close();
            }
        }, 5000);
    }

    function formatPrice(price) {
        const numPrice = parseFloat(price);
        if (typeof numPrice !== 'number' || isNaN(numPrice)) {
            return 'N/A';
        }
        return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL',
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        }).format(numPrice);
    }

    function setLoadingState(button, isLoading, defaultTextWithIcon) {
        if (!button) return;
        if (isLoading) {
            button.disabled = true;
            button.innerHTML = `<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Carregando...`;
        } else {
            button.disabled = false;
            button.innerHTML = defaultTextWithIcon;
        }
    }

    function resetAllFormsAndStates() {
        imovelForm.reset();
        imovelIdField.value = '';
        imovelFormCardTitle.innerHTML = '<i class="bi bi-plus-circle-fill me-2"></i>Adicionar Novo Imóvel';
        imovelSubmitButton.textContent = 'Adicionar Imóvel';
        imovelCancelEditButton.style.display = 'none';

        servicoForm.reset();
        servicoIdField.value = '';
        servicoFormCardTitle.innerHTML = '<i class="bi bi-plus-circle-fill me-2"></i>Adicionar Novo Serviço';
        servicoSubmitButton.textContent = 'Adicionar Serviço';
        servicoCancelEditButton.style.display = 'none';

        editingId = null;
    }

    // --- Lógica de Abas ---
    imoveisTabButton.addEventListener('click', () => {
        currentTab = 'imoveis';
        editingId = null;
        resetAllFormsAndStates();
        fetchImoveis();
    });
    servicosTabButton.addEventListener('click', () => {
        currentTab = 'servicos';
        editingId = null;
        resetAllFormsAndStates();
        fetchServicos();
    });

    // --- Funções CRUD para Imóveis (sem alterações nesta seção) ---
    async function fetchImoveis() {
        setLoadingState(loadImoveisButton, true, '<i class="bi bi-arrow-clockwise"></i>');
        try {
            const response = await fetch(API_IMOVEIS_URL);
            if (!response.ok) throw new Error(`HTTP ${response.status}: ${await response.text()}`);
            const imoveis = await response.json();
            renderImoveis(imoveis);
        } catch (error) {
            showMessage(`Falha ao buscar imóveis: ${error.message}`, 'error');
            imoveisListEl.innerHTML = '<p class="text-center text-danger p-3">Erro ao carregar imóveis.</p>';
        } finally {
            setLoadingState(loadImoveisButton, false, '<i class="bi bi-arrow-clockwise"></i> Recarregar');
        }
    }

    function renderImoveis(imoveis) {
        imoveisListEl.innerHTML = '';
        if (!imoveis || imoveis.length === 0) {
            imoveisListEl.innerHTML = '<div class="text-center text-muted p-3"><i class="bi bi-building-slash fs-3 d-block mb-2"></i>Nenhum imóvel encontrado.</div>';
            return;
        }
        imoveis.forEach(imovel => {
            const item = document.createElement('div');
            item.className = 'list-group-item list-group-item-action flex-column align-items-start mb-2 border rounded p-3';
            item.innerHTML = `
                <div class="d-flex w-100 justify-content-between">
                    <h5 class="mb-1 fw-semibold">${imovel.titulo}</h5>
                    <small class="text-muted">ID: ${imovel.id}</small>
                </div>
                <div class="property-details">
                    <p class="mb-1">${imovel.descricao || '<em>Sem descrição</em>'}</p>
                    <p class="mb-1 fw-bold text-success">${formatPrice(imovel.preco)}</p>
                    <p class="mb-1">
                        <span class="badge ${imovel.publicado ? 'bg-success-subtle text-success-emphasis' : 'bg-secondary-subtle text-secondary-emphasis'}">
                            ${imovel.publicado ? '<i class="bi bi-eye-fill me-1"></i>Publicado' : '<i class="bi bi-eye-slash-fill me-1"></i>Rascunho'}
                        </span>
                    </p>
                    ${imovel.dataCadastro ? `<small class="text-muted d-block mt-1">Cadastrado: ${new Date(imovel.dataCadastro).toLocaleDateString('pt-BR')}</small>` : ''}
                </div>
                <div class="actions mt-2 text-end">
                    <button class="btn btn-sm btn-outline-primary edit-imovel-btn" data-id="${imovel.id}" title="Editar"><i class="bi bi-pencil-square"></i> Editar</button>
                    <button class="btn btn-sm btn-outline-danger delete-imovel-btn" data-id="${imovel.id}" title="Excluir"><i class="bi bi-trash3-fill"></i> Excluir</button>
                </div>
            `;
            imoveisListEl.appendChild(item);
        });
        imoveisListEl.querySelectorAll('.edit-imovel-btn').forEach(b => b.addEventListener('click', (e) => populateImovelFormForEdit(e.currentTarget.dataset.id)));
        imoveisListEl.querySelectorAll('.delete-imovel-btn').forEach(b => b.addEventListener('click', (e) => handleDeleteImovel(e.currentTarget.dataset.id)));
    }

    imovelForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const titulo = imovelTituloField.value;
        const descricao = imovelDescricaoField.value;
        const preco = parseFloat(imovelPrecoField.value);
        const publicado = imovelPublicadoField.checked;

        if (!titulo.trim()) return showMessage('Título é obrigatório.', 'error');
        if (isNaN(preco) || preco < 0) return showMessage('Preço inválido.', 'error');

        const imovelData = { titulo, descricao: descricao || null, preco, publicado };
        const method = editingId ? 'PUT' : 'POST';
        const url = editingId ? `${API_IMOVEIS_URL}/${editingId}` : API_IMOVEIS_URL;

        setLoadingState(imovelSubmitButton, true, '...');
        try {
            const response = await fetch(url, { method, headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(imovelData) });
            if (!response.ok) {
                const errData = await response.json().catch(() => ({ message: `Erro ${response.status} - ${response.statusText}` }));
                throw new Error(errData.message || errData.title || `Erro ${response.status}`);
            }
            showMessage(`Imóvel ${editingId ? 'atualizado' : 'criado'}!`, 'success');
            resetAllFormsAndStates();
            fetchImoveis();
        } catch (error) {
            showMessage(`Falha ao salvar imóvel: ${error.message}`, 'error');
        } finally {
            setLoadingState(imovelSubmitButton, false, editingId ? 'Atualizar Imóvel' : 'Adicionar Imóvel');
        }
    });

    async function populateImovelFormForEdit(id) {
        try {
            const response = await fetch(`${API_IMOVEIS_URL}/${id}`);
            if (!response.ok) throw new Error('Imóvel não encontrado para edição.');
            const imovel = await response.json();

            imovelIdField.value = imovel.id;
            imovelTituloField.value = imovel.titulo;
            imovelDescricaoField.value = imovel.descricao || '';
            imovelPrecoField.value = imovel.preco;
            imovelPublicadoField.checked = imovel.publicado;

            editingId = imovel.id;
            imovelFormCardTitle.innerHTML = '<i class="bi bi-pencil-fill me-2"></i>Editar Imóvel';
            imovelSubmitButton.textContent = 'Atualizar Imóvel';
            imovelCancelEditButton.style.display = 'inline-block';
            imovelForm.scrollIntoView({ behavior: 'smooth', block: 'start' });
        } catch (error) { showMessage(error.message, 'error'); }
    }

    async function handleDeleteImovel(id) {
        if (!confirm(`Excluir imóvel ID ${id}?`)) return;
        try {
            const response = await fetch(`${API_IMOVEIS_URL}/${id}`, { method: 'DELETE' });
            if (!response.ok) {
                const errData = await response.json().catch(() => ({ message: `Erro ${response.status} - ${response.statusText}` }));
                throw new Error(errData.message || errData.title || `Erro ${response.status}`);
            }
            showMessage('Imóvel deletado!', 'success');
            if (editingId && editingId === parseInt(id)) resetAllFormsAndStates();
            fetchImoveis();
        } catch (error) { showMessage(`Falha ao deletar imóvel: ${error.message}`, 'error'); }
    }
    imovelCancelEditButton.addEventListener('click', resetAllFormsAndStates);
    loadImoveisButton.addEventListener('click', fetchImoveis);

    // --- Funções CRUD para Serviços (COM AJUSTES PARA precoBase) ---
    async function fetchServicos() {
        setLoadingState(loadServicosButton, true, '<i class="bi bi-arrow-clockwise"></i>');
        try {
            const response = await fetch(API_SERVICOS_URL);
            if (!response.ok) throw new Error(`HTTP ${response.status}: ${await response.text()}`);
            const servicos = await response.json();
            renderServicos(servicos);
        } catch (error) {
            showMessage(`Falha ao buscar serviços: ${error.message}`, 'error');
            servicosListEl.innerHTML = '<p class="text-center text-danger p-3">Erro ao carregar serviços.</p>';
        } finally {
            setLoadingState(loadServicosButton, false, '<i class="bi bi-arrow-clockwise"></i> Recarregar');
        }
    }

    function renderServicos(servicos) {
        servicosListEl.innerHTML = '';
        if (!servicos || servicos.length === 0) {
            servicosListEl.innerHTML = '<div class="text-center text-muted p-3"><i class="bi bi-tools fs-3 d-block mb-2"></i>Nenhum serviço encontrado.</div>';
            return;
        }
        servicos.forEach(servico => {
            const item = document.createElement('div');
            item.className = 'list-group-item list-group-item-action flex-column align-items-start mb-2 border rounded p-3';
            // AJUSTE AQUI: Usando servico.precoBase para formatPrice
            item.innerHTML = `
                <div class="d-flex w-100 justify-content-between">
                    <h5 class="mb-1 fw-semibold">${servico.nome}</h5>
                    <small class="text-muted">ID: ${servico.id}</small>
                </div>
                <div class="property-details">
                    <p class="mb-1">${servico.descricao || '<em>Sem descrição</em>'}</p>
                    <p class="mb-1 fw-bold text-info">${formatPrice(servico.precoBase)}</p>
                    <p class="mb-1">
                        <span class="badge ${servico.ativo ? 'bg-success-subtle text-success-emphasis' : 'bg-secondary-subtle text-secondary-emphasis'}">
                            ${servico.ativo ? '<i class="bi bi-check-circle-fill me-1"></i>Ativo' : '<i class="bi bi-x-circle-fill me-1"></i>Inativo'}
                        </span>
                    </p>
                     ${servico.dataCadastro ? `<small class="text-muted d-block mt-1">Cadastrado: ${new Date(servico.dataCadastro).toLocaleDateString('pt-BR')}</small>` : ''}
                </div>
                <div class="actions mt-2 text-end">
                    <button class="btn btn-sm btn-outline-primary edit-servico-btn" data-id="${servico.id}" title="Editar"><i class="bi bi-pencil-square"></i> Editar</button>
                    <button class="btn btn-sm btn-outline-danger delete-servico-btn" data-id="${servico.id}" title="Excluir"><i class="bi bi-trash3-fill"></i> Excluir</button>
                </div>
            `;
            servicosListEl.appendChild(item);
        });
        servicosListEl.querySelectorAll('.edit-servico-btn').forEach(b => b.addEventListener('click', (e) => populateServicoFormForEdit(e.currentTarget.dataset.id)));
        servicosListEl.querySelectorAll('.delete-servico-btn').forEach(b => b.addEventListener('click', (e) => handleDeleteServico(e.currentTarget.dataset.id)));
    }

    servicoForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const nome = servicoNomeField.value;
        const descricao = servicoDescricaoField.value;
        const precoInput = servicoPrecoField.value; // Valor do input
        const ativo = servicoAtivoField.checked;

        if (!nome.trim()) return showMessage('Nome do serviço é obrigatório.', 'error');

        let precoNum;
        if (precoInput.trim() === '') { // Se o campo preço estiver vazio
            // Você precisa decidir se preço vazio é permitido e o que enviar para a API.
            // Se o backend espera null para precoBase e aceita isso:
            // precoNum = null; 
            // OU se o backend espera um número e 0 é aceitável para "sem preço":
            // precoNum = 0;
            // OU se é obrigatório (como nos DTOs que esperam 'preco'):
            return showMessage('Preço é obrigatório para o serviço.', 'error');
        } else {
            precoNum = parseFloat(precoInput);
            if (isNaN(precoNum) || precoNum < 0) {
                return showMessage('Preço inválido para o serviço.', 'error');
            }
        }

        // API espera 'preco', não 'precoBase', para POST/PUT, conforme DTOs.
        const servicoData = {
            nome,
            descricao: descricao || null,
            preco: precoNum, // Enviando como 'preco'
            ativo
        };
        const method = editingId ? 'PUT' : 'POST';
        const url = editingId ? `${API_SERVICOS_URL}/${editingId}` : API_SERVICOS_URL;

        setLoadingState(servicoSubmitButton, true, '...');
        try {
            const response = await fetch(url, { method, headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(servicoData) });
            if (!response.ok) {
                const errData = await response.json().catch(() => ({ message: `Erro ${response.status} - ${response.statusText}` }));
                throw new Error(errData.message || errData.title || `Erro ${response.status}`);
            }
            showMessage(`Serviço ${editingId ? 'atualizado' : 'criado'}!`, 'success');
            resetAllFormsAndStates();
            fetchServicos();
        } catch (error) {
            showMessage(`Falha ao salvar serviço: ${error.message}`, 'error');
        } finally {
            setLoadingState(servicoSubmitButton, false, editingId ? 'Atualizar Serviço' : 'Adicionar Serviço');
        }
    });

    async function populateServicoFormForEdit(id) {
        try {
            const response = await fetch(`${API_SERVICOS_URL}/${id}`);
            if (!response.ok) throw new Error('Serviço não encontrado para edição.');
            const servico = await response.json();

            servicoIdField.value = servico.id;
            servicoNomeField.value = servico.nome;
            servicoDescricaoField.value = servico.descricao || '';
            // AJUSTE AQUI: Popular formulário com servico.precoBase
            servicoPrecoField.value = servico.precoBase !== null && servico.precoBase !== undefined ? servico.precoBase : ''; // <--- MUDANÇA AQUI
            servicoAtivoField.checked = servico.ativo;

            editingId = servico.id;
            servicoFormCardTitle.innerHTML = '<i class="bi bi-pencil-fill me-2"></i>Editar Serviço';
            servicoSubmitButton.textContent = 'Atualizar Serviço';
            servicoCancelEditButton.style.display = 'inline-block';
            servicoForm.scrollIntoView({ behavior: 'smooth' });
        } catch (error) { showMessage(error.message, 'error'); }
    }

    async function handleDeleteServico(id) {
        if (!confirm(`Excluir serviço ID ${id}?`)) return;
        try {
            const response = await fetch(`${API_SERVICOS_URL}/${id}`, { method: 'DELETE' });
            if (!response.ok) {
                const errData = await response.json().catch(() => ({ message: `Erro ${response.status} - ${response.statusText}` }));
                throw new Error(errData.message || errData.title || `Erro ${response.status}`);
            }
            showMessage('Serviço deletado!', 'success');
            if (editingId && editingId === parseInt(id)) resetAllFormsAndStates();
            fetchServicos();
        } catch (error) { showMessage(`Falha ao deletar serviço: ${error.message}`, 'error'); }
    }
    servicoCancelEditButton.addEventListener('click', resetAllFormsAndStates);
    loadServicosButton.addEventListener('click', fetchServicos);

    // --- Inicialização ---
    function initializeApp() {
        const imoveisTabTriggerEl = document.getElementById('imoveis-tab');
        const servicosTabTriggerEl = document.getElementById('servicos-tab');

        if (imoveisTabTriggerEl) {
            const imoveisTabTrigger = new bootstrap.Tab(imoveisTabTriggerEl);
            imoveisTabTriggerEl.addEventListener('show.bs.tab', function () {
                currentTab = 'imoveis';
                editingId = null;
                resetAllFormsAndStates();
                // fetchImoveis(); // Já é chamado pelo 'shown.bs.tab' ou load inicial
            });
        }
        if (servicosTabTriggerEl) {
            const servicosTabTrigger = new bootstrap.Tab(servicosTabTriggerEl);
            servicosTabTriggerEl.addEventListener('show.bs.tab', function () {
                currentTab = 'servicos';
                editingId = null;
                resetAllFormsAndStates();
                // fetchServicos(); // Já é chamado pelo 'shown.bs.tab' ou load inicial
            });
        }

        // Carrega dados da aba ativa inicialmente
        if (currentTab === 'imoveis') {
            fetchImoveis();
        } else {
            fetchServicos();
        }
    }
    initializeApp();
});