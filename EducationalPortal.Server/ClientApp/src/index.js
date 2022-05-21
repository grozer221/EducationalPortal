import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import * as serviceWorkerRegistration from './serviceWorkerRegistration';
import reportWebVitals from './reportWebVitals';
import {App} from './App';
import {store} from './store/store';
import {client} from './gql/client';
import {BrowserRouter} from 'react-router-dom';
import {ApolloProvider} from '@apollo/client';
import {ConfigProvider} from 'antd';
import ukUA from 'antd/lib/locale/uk_UA';
import {Provider} from 'react-redux';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
    <React.StrictMode>
        <Provider store={store}>
            <BrowserRouter basename={baseUrl}>
                <ApolloProvider client={client}>
                    <ConfigProvider locale={ukUA}>
                        <App />
                    </ConfigProvider>
                </ApolloProvider>
            </BrowserRouter>
        </Provider>
    </React.StrictMode>,
  rootElement);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();