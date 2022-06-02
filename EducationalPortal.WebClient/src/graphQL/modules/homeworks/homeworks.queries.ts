import {gql} from '@apollo/client';
import {HOMEWORK_FRAGMENT} from './homeworks.fragments';
import {Homework, HomeworkStatus} from './homework.types';
import {Order} from '../../enums/order';

export type GetHomeworkData = { getHomework: Homework }
export type GetHomeworkVars = { id: string, withFiles: boolean }

export const GET_HOMEWORK_QUERY = gql`
    ${HOMEWORK_FRAGMENT}
    query GetHomework($id: ID!, $withFiles: Boolean!) {
        getHomework(id: $id) {
            ...HomeworkFragment
        }
    }
`;


export type GetHomeworksData = { getHomeworks: getHomeworksType }
export type getHomeworksType = { entities: Homework[], total: number, pageSize: number }

export type GetHomeworksVars = { page: number, statuses: HomeworkStatus[] | null | undefined, order: Order, subjectPostId?: string | null, withFiles: boolean}

export const GET_HOMEWORKS_QUERY = gql`
    ${HOMEWORK_FRAGMENT}
    query GetHomeworks($page: Int!, $statuses: [HomeworkStatus], $order: Order!, $subjectPostId: Guid, $withFiles: Boolean!) {
        getHomeworks(page: $page, statuses: $statuses, order: $order, subjectPostId: $subjectPostId) {
            entities {
                ...HomeworkFragment
            }
            total
            pageSize
        }
    }
`;
