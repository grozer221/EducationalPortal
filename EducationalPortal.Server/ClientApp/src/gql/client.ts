import {ApolloClient, HttpLink, InMemoryCache, split} from '@apollo/client';
import {schema} from './schema';
import {WebSocketLink} from '@apollo/client/link/ws';
import {getMainDefinition} from '@apollo/client/utilities';
import {setContext} from '@apollo/client/link/context';

const authLink = setContext((_, {headers}) => ({
    headers: {
        ...headers,
        authorization: `Bearer ${localStorage.getItem('token') ?? ''}`,
    },
}));

const httpLink = new HttpLink({uri: !process.env.NODE_ENV || process.env.NODE_ENV === 'development' ? `https://localhost:7246/graphql` : `./graphql`});

const wsLink = new WebSocketLink({
    uri: !process.env.NODE_ENV || process.env.NODE_ENV === 'development' ? `wss://localhost:7246/graphql` : `wss://${window.location.host}/graphql`,
    options: {
        reconnect: true,
        connectionParams: {
            authToken: localStorage.getItem('token') ? `Bearer ${localStorage.getItem('token')}` : '',
        },
    },
});

const splitLink = split(
    ({query}) => {
        const definition = getMainDefinition(query);
        return (definition.kind === 'OperationDefinition' && definition.operation === 'subscription');
    },
    wsLink,
    authLink.concat(httpLink),
);

export const client = new ApolloClient({
    link: splitLink,
    cache: new InMemoryCache(),
    defaultOptions: {
        watchQuery: {
            // fetchPolicy: 'network-only',
            errorPolicy: 'all',
            notifyOnNetworkStatusChange: true,
        },
        query: {
            // fetchPolicy: 'network-only',
            errorPolicy: 'all',
            notifyOnNetworkStatusChange: true,
        },
    },
    typeDefs: schema,
});
