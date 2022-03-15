import React from 'react';
import ReactDOM from 'react-dom';
import {Provider} from 'react-redux';
import {App} from './App';
import {store} from './store/store';
import {client} from './gql/client';
import {BrowserRouter} from 'react-router-dom';
import {ApolloProvider} from '@apollo/client';
import {ConfigProvider} from 'antd';
import ukUA from 'antd/lib/locale/uk_UA';

ReactDOM.render(
    <React.StrictMode>
        <Provider store={store}>
            <BrowserRouter>
                <ApolloProvider client={client}>
                    <ConfigProvider locale={ukUA}>
                        <App />
                    </ConfigProvider>
                </ApolloProvider>
            </BrowserRouter>
        </Provider>
    </React.StrictMode>,
    document.getElementById('root'));
