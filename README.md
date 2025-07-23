# Gestão de Pedidos WPF

Aplicação desktop desenvolvida em WPF (.NET Framework 4.6) para gerenciamento de pedidos, pessoas e produtos. Os dados são armazenados localmente em arquivos JSON, sem necessidade de banco de dados ou configuração adicional.

## 🛠 Tecnologias Utilizadas

- .NET Framework 4.6
- WPF (Windows Presentation Foundation)
- MVVM (Model-View-ViewModel)
- Armazenamento em JSON
- [Newtonsoft.Json](https://www.newtonsoft.com/json) para serialização

## 📋 Funcionalidades

- Cadastro de Pessoas, Produtos e Pedidos
- Telas de visualização com filtros
- Indicadores no Dashboard

## 💾 Armazenamento de Dados

Todos os dados são salvos automaticamente em arquivos `.json` assim que as entidades são criadas ou editadas.

Não é necessário configurar banco de dados ou arquivos manualmente.

Exemplo de estrutura de dados:
```
/Data/
├── pessoas.json
├── produtos.json
└── pedidos.json
```

## 🚀 Como Usar

1. Abra o projeto no Visual Studio
2. Execute (F5)
3. Comece a cadastrar pessoas, produtos e pedidos — os arquivos serão criados automaticamente.
