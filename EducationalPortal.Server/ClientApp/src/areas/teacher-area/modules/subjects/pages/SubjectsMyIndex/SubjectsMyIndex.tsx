import React, {useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {ColumnsType} from 'antd/es/table';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {message, Space, Table} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {Link} from 'react-router-dom';
import {GET_MY_SUBJECTS_QUERY, GetMySubjectsData, GetMySubjectsVars} from '../../subjects.queries';
import {Subject} from '../../subjects.types';
import {REMOVE_SUBJECT_MUTATION, RemoveSubjectData, RemoveSubjectVars} from '../../subjects.mutations';
import Title from 'antd/es/typography/Title';

export const SubjectsMyIndex = () => {
    const [page, setPage] = useState(1);
    const [like, setLike] = useState('');
    const getSubjectsQuery = useQuery<GetMySubjectsData, GetMySubjectsVars>(GET_MY_SUBJECTS_QUERY,
        {variables: {page: page, like: like}, fetchPolicy: 'network-only'},
    );
    const [removeSubjectMutation, removeSubjectMutationOptions] = useMutation<RemoveSubjectData, RemoveSubjectVars>(REMOVE_SUBJECT_MUTATION);

    const onRemove = (subjectId: string) => {
        removeSubjectMutation({variables: {id: subjectId}})
            .then(async (response) => {
                await getSubjectsQuery.refetch({page});
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    const columns: ColumnsType<Subject> = [
        {
            title: 'Назва',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Навчальний рік',
            dataIndex: 'educationalYear',
            key: 'educationalYear',
            render: (text, subject) => <Link
                to={`../../educational-years/${subject?.educationalYearId}`}>{subject?.educationalYear?.name}</Link>,
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            render: (text: string, subject: Subject) => (
                <ButtonsVUR viewUrl={`../${subject?.id}`} updateUrl={`../update/${subject?.id}`}
                            onRemove={() => onRemove(subject?.id)}/>
            ),
        },
    ];

    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title level={2}>Мої предмети</Title>
            <Link to={'../create'}>
                <ButtonCreate/>
            </Link>
            <Table
                style={{width: '100%'}}
                rowKey={'id'}
                loading={getSubjectsQuery.loading || removeSubjectMutationOptions.loading}
                dataSource={getSubjectsQuery.data?.getMySubjects.entities}
                columns={columns}
                pagination={{
                    total: getSubjectsQuery.data?.getMySubjects.total,
                    onChange: async (newPage: number) => {
                        setPage(newPage);
                        await getSubjectsQuery.refetch({page: newPage});
                    },
                }}
            />
        </Space>
    );
};
